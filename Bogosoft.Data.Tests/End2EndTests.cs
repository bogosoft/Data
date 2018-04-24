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

        static List<FieldAdapter<CelestialBody>> Fields = new List<FieldAdapter<CelestialBody>>();

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
#if NET45
                Type = (CelestialBodyType)Enum.Parse(typeof(CelestialBodyType), reader.GetFieldValue<string>(Type))
#else
                Type = Enum.Parse<CelestialBodyType>(reader.GetFieldValue<string>(Type))
#endif
            };
        }

        [OneTimeSetUp]
        public void Setup()
        {
            Fields.Add(new FieldAdapter<CelestialBody>(Name, typeof(string), x => x.Name));
            Fields.Add(new FieldAdapter<CelestialBody>(Type, typeof(string), x => x.Type.ToString()));
            Fields.Add(new FieldAdapter<CelestialBody>(Mass, typeof(float), x => x.Mass));
            Fields.Add(new FieldAdapter<CelestialBody>(PrimaryDistance, typeof(float), x => x.Orbit.DistanceToPrimary));
        }

        [TestCase]
        public void CanConvertDbDataReaderToEnumerableAndBack()
        {
            new CelestialBodyDataReader().ToEnumerable(ReadCelestialBody)
                                         .ToDbDataReader(Fields)
                                         .ShouldHaveSameDataAs(new CelestialBodyDataReader());
        }

        [TestCase]
        public void CanConvertEnumerableToDbDataReaderAndBack()
        {
            var expected = CelestialBody.All;

            var actual = expected.ToDbDataReader(Fields).ToEnumerable(ReadCelestialBody);

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