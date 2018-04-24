using NUnit.Framework;
using Shouldly;
using System;

namespace Bogosoft.Data.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
        [TestCase]
        public void ColumnCreatedWithTwoGenericParametersDoesNotThrowNullReferenceExceptionOnExtract()
        {
            var column = new Column<DateTime, DayOfWeek>("day_of_week", x => x.DayOfWeek);

            DayOfWeek result = default(DayOfWeek);

            Action actual = () => result = (DayOfWeek)column.ExtractValueFrom(DateTime.Today);

            actual.ShouldNotThrow();

            result.ShouldBe(DateTime.Today.DayOfWeek);
        }
    }
}