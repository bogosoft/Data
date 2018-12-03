using Bogosoft.Testing.Objects;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Bogosoft.Data.Tests
{
    [TestFixture, Category("Unit")]
    public class End2EndTests
    {
        const string Mass = "Mass";
        const string Name = "Name";
        const string PrimaryDistance = "Distance to Primary";
        const string Type = "Type";

        static List<FieldAdapter<CelestialBody>> Fields = new List<FieldAdapter<CelestialBody>>();

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
                Type = (CelestialBodyType)Enum.Parse(typeof(CelestialBodyType), reader.GetFieldValue<string>(Type))
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
            DbDataReader actual, expected = new CelestialBodyDataReader();

            actual = new CelestialBodyDataReader().ToEnumerable(ReadCelestialBody).ToDbDataReader(Fields);

            actual.FieldCount.ShouldBe(expected.FieldCount);

            while (actual.Read())
            {
                expected.Read().ShouldBeTrue();

                for (var i = 0; i < expected.FieldCount; i++)
                {
                    actual[i].ShouldBe(expected[i]);

                    actual.GetValue(i).ShouldBe(expected.GetValue(i));
                }
            }

            expected.Read().ShouldBeFalse();
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

                    a.Current.ShouldNotBeNull();
                    e.Current.ShouldNotBeNull();

                    a.Current.Mass.ShouldBe(e.Current.Mass);
                    a.Current.Name.ShouldBe(e.Current.Name);

                    if (a.Current.Orbit is null)
                    {
                        e.Current.Orbit.ShouldBeNull();
                    }
                    else
                    {
                        a.Current.Orbit.DistanceToPrimary.ShouldBe(e.Current.Orbit.DistanceToPrimary);
                    }

                    a.Current.Type.ShouldBe(e.Current.Type);
                }

                e.MoveNext().ShouldBeFalse();
            }
        }
    }
}