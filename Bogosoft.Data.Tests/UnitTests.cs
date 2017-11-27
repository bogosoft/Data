using Bogosoft.Testing.Objects;
using NUnit.Framework;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bogosoft.Data.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
        const string One = "1";
        const string Two = "2";
        const string Three = "3";

        static IEnumerable<string[]> Rows
        {
            get
            {
                yield return new[] { "First", "Second", "Third" };
                yield return new[] { One, Two, Three };
            }
        }

        [TestCase]
        public void ObjectArraySequenceDataReaderInfersColumnNamesCorrectly()
        {
            var columns = new string[] { One, Two, Three };
            var records = new List<string[]> { columns };

            using (var reader = records.ToDataReader())
            {
                for (var i = 0; i < columns.Length; i++)
                {
                    reader.GetName(i).ShouldEqual(columns[i]);
                }

                reader.Read().ShouldBeFalse();
            }
        }

        [TestCase]
        public void ObjectArraySequenceDataReaderRetrievesFieldValueByColumnNameCorrectly()
        {
            var records = new List<string[]>();

            var columns = new string[] { "First", "Second", "Third" };

            records.Add(columns);

            var row = new string[] { One, Two, Three };

            records.Add(row);

            using (var reader = records.ToDataReader())
            {
                reader.Read().ShouldBeTrue();

                for (var i = 0; i < columns.Length; i++)
                {
                    reader[i].ShouldEqual(row[i]);

                    reader[columns[i]].ShouldEqual(row[i]);
                }

                reader.Read().ShouldBeFalse();
            }
        }

        [TestCase]
        public void ParsingDataReaderWorksAsExpected()
        {
            var records = new List<string[]>();

            records.Add(new[] { "Sample Date", "Sample Size" });
            records.Add(new[] { "2010-01-01", "256" });

            var parsers = new Parser[] { x => DateTime.Parse(x), x => int.Parse(x) };

            using (var reader = records.ToDataReader(parsers))
            {
                reader.Read().ShouldBeTrue();

                reader["Sample Date"].ShouldBeType<DateTime>();

                reader.GetDateTime(reader.GetOrdinal("Sample Date")).ShouldEqual(DateTime.Parse("2010-01-01"));

                reader["Sample Size"].ShouldBeType<int>();

                reader.GetInt32(reader.GetOrdinal("Sample Size")).ShouldEqual(int.Parse("256"));

                reader.Read().ShouldBeFalse();
            }
        }

        [TestCase]
        public void PrependedDataReaderReturnsNewValueFromFirstOrdinalPosition()
        {
            using (var reader = Rows.ToDataReader())
            {
                reader.Read().ShouldBeTrue();

                reader.GetFieldValue<string>(0).ShouldEqual(One);
            }

            var added = DateTime.UtcNow.ToString();

            using (var reader = Rows.ToDataReader().Prepend("Zeroeth", added))
            {
                reader.Read().ShouldBeTrue();

                reader.GetFieldValue<string>(0).ShouldEqual(added);
                reader.GetFieldValue<string>(1).ShouldEqual(One);
            }
        }

        [TestCase]
        public void StringValuedRecordParsingWorksAsExpected()
        {
            var records = new List<string[]>();

            records.Add(new[] { "Sample Date", "Sample Size" });
            records.Add(new[] { "2010-01-01", "256" });

            var parsers = new Parser[] { x => DateTime.Parse(x), x => int.Parse(x) };

            using (var reader = records.Parse(parsers).ToDataReader())
            {
                reader.Read().ShouldBeTrue();

                reader["Sample Date"].ShouldBeType<DateTime>();

                reader.GetDateTime(reader.GetOrdinal("Sample Date")).ShouldEqual(DateTime.Parse("2010-01-01"));

                reader["Sample Size"].ShouldBeType<int>();

                reader.GetInt32(reader.GetOrdinal("Sample Size")).ShouldEqual(int.Parse("256"));

                reader.Read().ShouldBeFalse();
            }
        }

        [TestCase]
        public void TypedSequenceDataReaderWorksAsExpected()
        {
            IEnumerable<CelestialBody> celestialBodies = CelestialBody.All.ToArray();

            var columns = new[] { "Name", "Type", "Mass" };

            var extractors = new ValueExtractor<CelestialBody>[]
            {
                x => x.Name,
                x => x.Mass,
                x => x.Type.ToString()
            };

            using (var reader = celestialBodies.ToDataReader(columns, extractors))
            using (var enumerator = celestialBodies.GetEnumerator())
            {
                while (reader.Read())
                {
                    enumerator.MoveNext().ShouldBeTrue();

                    for (var i = 0; i < columns.Length; i++)
                    {
                        reader.GetValue(i).ShouldEqual(extractors[i].Invoke(enumerator.Current));

                        reader[i].ShouldEqual(extractors[i].Invoke(enumerator.Current));

                        reader[columns[i]].ShouldEqual(extractors[i].Invoke(enumerator.Current));
                    }
                }

                enumerator.MoveNext().ShouldBeFalse();
            }
        }
    }
}