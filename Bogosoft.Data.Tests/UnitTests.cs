using NUnit.Framework;
using Shouldly;
using System;

namespace Bogosoft.Data.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
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