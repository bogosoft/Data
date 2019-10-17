using Bogosoft.Testing.Objects;
using System;
using System.Collections.Generic;
using System.Data;

namespace Bogosoft.Data.Tests
{
    class CelestialBodyDataReader : SimplifiedDataReader
    {
        IEnumerator<CelestialBody> enumerator = CelestialBody.All.GetEnumerator();
        readonly DataTable schemaTable;

        public override object this[int ordinal] => GetValue(ordinal);

        public override int Depth => 0;

        public override int FieldCount => 4;

        public override bool HasRows => true;

        public override bool IsClosed => enumerator == null;

        public override int RecordsAffected => 0;

        internal CelestialBodyDataReader()
        {
            var columns = (schemaTable = new DataTable()).Columns;

            for (var i = 0; i < FieldCount; i++)
            {
                columns.Add(GetName(i), GetFieldType(i));
            }
        }

        public override void Close()
        {
            enumerator = null;
        }

        protected override void Dispose(bool disposing)
        {
            enumerator?.Dispose();

            schemaTable?.Dispose();

            base.Dispose(disposing);
        }

        public override Type GetFieldType(int ordinal)
        {
            switch (ordinal)
            {
                case 0:
                case 1:
                    return typeof(string);
                case 2:
                case 3:
                    return typeof(float);
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        public override string GetName(int ordinal)
        {
            return ordinal switch
            {
                0 => "Name",
                1 => "Type",
                2 => "Mass",
                3 => "Distance to Primary",
                _ => throw new IndexOutOfRangeException(),
            };
        }

        public override int GetOrdinal(string name)
        {
            return (name.ToLower()) switch
            {
                "distance to primary" => 3,
                "mass" => 2,
                "name" => 0,
                "type" => 1,
                _ => throw new IndexOutOfRangeException(),
            };
        }

        public override DataTable GetSchemaTable() => schemaTable;

        public override long GetValue<T>(int ordinal, long dataOffset, T[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override object GetValue(int ordinal)
        {
            return ordinal switch
            {
                0 => enumerator.Current.Name,
                1 => enumerator.Current.Type.ToString(),
                2 => enumerator.Current.Mass,
                3 => enumerator.Current.Orbit.DistanceToPrimary,
                _ => throw new IndexOutOfRangeException(),
            };
        }

        public override int GetValues(object[] values)
        {
            values[0] = enumerator.Current.Name;
            values[1] = enumerator.Current.Type.ToString();
            values[2] = enumerator.Current.Mass;
            values[3] = enumerator.Current.Orbit.DistanceToPrimary;

            return 4;
        }

        public override bool IsDBNull(int ordinal)
        {
            switch (ordinal)
            {
                case 0:
                    return enumerator.Current.Name is null;
                case 1:
                case 2:
                case 3:
                    return false;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        public override bool NextResult() => false;

        public override bool Read() => enumerator.MoveNext();
    }
}