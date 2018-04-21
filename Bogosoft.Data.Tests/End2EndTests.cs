using Bogosoft.Testing.Objects;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Bogosoft.Data.Tests
{
    [TestFixture, Category("End2End")]
    public class End2EndTests
    {
        const string Mass = "Mass";
        const string Name = "Name";
        const string PrimaryDistance = "Distance to Primary";
        const string Type = "Type";

        static List<Column<CelestialBody>> Columns = new List<Column<CelestialBody>>();

        static bool AreEqual(CelestialBody a, CelestialBody b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            else
            {
                return a.Mass == b.Mass
                    && a.Name == b.Name
                    && a.Orbit?.DistanceToPrimary == b.Orbit?.DistanceToPrimary
                    && a.Type == b.Type;
            }
        }

        static CelestialBody ReadCelestialBody(DbDataReader reader)
        {
            return new CelestialBody
            {
                Mass = reader.GetFieldValue<float>(Mass),
                Name = reader.GetFieldValue<string>(Name),
                Orbit = new OrbitalInfo
                {
                    DistanceToPrimary = reader.GetFieldValue<float>(PrimaryDistance)
                },
                Type = Enum.Parse<CelestialBodyType>(reader.GetFieldValue<string>(Type))
            };
        }

        [OneTimeSetUp]
        public void Setup()
        {
            Columns.Add(new Column<CelestialBody>(Name, typeof(string), x => x.Name));
            Columns.Add(new Column<CelestialBody>(Type, typeof(string), x => x.Type.ToString()));
            Columns.Add(new Column<CelestialBody>(Mass, typeof(float), x => x.Mass));
            Columns.Add(new Column<CelestialBody>(PrimaryDistance, typeof(float), x => x.Orbit.DistanceToPrimary));
        }

        [TestCase]
        public void CanConvertDbDataReaderToEnumerableAndBack()
        {
            new CelestialBodyDataReader().ToEnumerable(ReadCelestialBody)
                                         .ToDbDataReader(Columns)
                                         .ShouldHaveSameDataAs(new CelestialBodyDataReader());
        }

        [TestCase]
        public void CanConvertEnumerableToDbDataReaderAndBack()
        {
            var expected = CelestialBody.All;

            var actual = expected.ToDbDataReader(Columns).ToEnumerable(ReadCelestialBody);

            using (var a = actual.GetEnumerator())
            using (var e = expected.GetEnumerator())
            {
                while (a.MoveNext())
                {
                    e.MoveNext().ShouldBeTrue();

                    AreEqual(a.Current, e.Current).ShouldBeTrue();
                }

                e.MoveNext().ShouldBeFalse();
            }
        }
    }
}