using Bogosoft.Collections.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Data
{
    class FirstResult : ITraversable<IRow>
    {
        struct Cursor : ICursor<IRow>
        {
            ICursor<IRow> inner;
            ICursor<IResult> outer;

            public bool IsDisposed => inner.IsDisposed;

            public bool IsExpended => inner.IsExpended;

            internal Cursor(ICursor<IRow> inner, ICursor<IResult> outer)
            {
                this.inner = inner;
                this.outer = outer;
            }

            public void Dispose()
            {
                if (!IsDisposed)
                {
                    inner.Dispose();
                    outer.Dispose();
                }
            }

            public async Task<IRow> GetCurrentAsync(CancellationToken token)
            {
                return await inner.GetCurrentAsync(token).ConfigureAwait(false);
            }

            public async Task<bool> MoveNextAsync(CancellationToken token)
            {
                return await inner.MoveNextAsync(token).ConfigureAwait(false);
            }
        }

        ITraversable<IResult> results;

        internal FirstResult(ITraversable<IResult> results)
        {
            this.results = results;
        }

        public async Task<ICursor<IRow>> GetCursorAsync(CancellationToken token)
        {
            var outer = await results.GetCursorAsync(token).ConfigureAwait(false);

            if(false == await outer.MoveNextAsync(token).ConfigureAwait(false))
            {
                return Cursor<IRow>.Empty;
            }

            var first = await outer.GetCurrentAsync(token).ConfigureAwait(false);

            return new Cursor(await first.GetCursorAsync(token).ConfigureAwait(false), outer);
        }
    }
}