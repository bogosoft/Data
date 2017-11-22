using NUnit.Framework;
using Should;
using System;
using System.Collections.Generic;

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
    }
}