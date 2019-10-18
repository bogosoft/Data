using Bogosoft.Testing.Objects;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bogosoft.Data.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
        [TestCase]
        public async Task CanCreateDataReaderFromAsyncSequence()
        {
            string mass = "Mass", name = "Name", type = "Type";

            var expected = CelestialBody.All.ToArray();

            var source = expected.AsAsync();

            source.ShouldBeAssignableTo<IAsyncEnumerable<CelestialBody>>();

            var fields = new FieldAdapter<CelestialBody>[]
            {
                new FieldAdapter<CelestialBody, string>(name, c => c.Name),
                new FieldAdapter<CelestialBody, float>(mass, c => c.Mass),
                new FieldAdapter<CelestialBody, string>(type, c => c.Type.ToString())
            };

            using var reader = source.ToDataReader(fields);

            var i = 0;

            while (await reader.ReadAsync())
            {
                var actualMass = await reader.GetFieldValueAsync<float>(mass, 0);
                var actualName = await reader.GetFieldValueAsync<string>(name, null);
                var actualType = await reader.GetFieldValueAsync<string>(type, null);

                actualMass.ShouldBe(expected[i].Mass);
                actualName.ShouldBe(expected[i].Name);
                actualType.ShouldBe(expected[i].Type.ToString());

                i += 1;
            }
        }

        [TestCase]
        public void CanCreateDataReaderFromSynchronousSequence()
        {
            string mass = "Mass", name = "Name", type = "Type";

            var expected = CelestialBody.All.ToArray();

            expected.ShouldBeAssignableTo<IEnumerable<CelestialBody>>();

            var fields = new FieldAdapter<CelestialBody>[]
            {
                new FieldAdapter<CelestialBody, string>(name, c => c.Name),
                new FieldAdapter<CelestialBody, float>(mass, c => c.Mass),
                new FieldAdapter<CelestialBody, string>(type, c => c.Type.ToString())
            };

            using var reader = expected.ToDataReader(fields);

            var i = 0;

            while (reader.Read())
            {
                var actualMass = reader.GetFieldValue<float>(mass, 0);
                var actualName = reader.GetFieldValue<string>(name, null);
                var actualType = reader.GetFieldValue<string>(type, null);

                actualMass.ShouldBe(expected[i].Mass);
                actualName.ShouldBe(expected[i].Name);
                actualType.ShouldBe(expected[i].Type.ToString());

                i += 1;
            }
        }

        [TestCase]
        public void FieldAdapterCreatedWithTwoGenericParametersDoesNotThrowNullReferenceExceptionOnExtract()
        {
            var field = new FieldAdapter<DateTime, DayOfWeek>("day_of_week", x => x.DayOfWeek);

            DayOfWeek result = default;

            Action actual = () => result = (DayOfWeek)field.ExtractValueFrom(DateTime.Today);

            actual.ShouldNotThrow();

            result.ShouldBe(DateTime.Today.DayOfWeek);
        }
    }
}