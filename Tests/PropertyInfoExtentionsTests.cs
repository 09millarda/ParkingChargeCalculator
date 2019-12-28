using FluentAssertions;
using ParkingChargeCalculator.Util;
using System;
using System.Linq;
using Xunit;

namespace Tests
{
    public class PropertyInfoExtentionsTests : TestBase
    {
        [Theory]
        [InlineData("ExampleDateTime", "2019-01-01T00:00:00", "01/01/2019 00:00:00")]
        [InlineData("ExampleEnum", "0", "ExampleA")]
        [InlineData("ExampleEnum", "ExampleA", "ExampleA")]
        [InlineData("ExampleEnum", "1", "ExampleB")]
        [InlineData("ExampleEnum", "ExampleB", "ExampleB")]
        public void Validly_formatted_strings_are_cast_and_added_to_the_object(string propertyName, string serializedValue, string expectedStringValue)
        {
            var exampleObject = new PropertyInfoExtensionsTestClass();

            var propertyInfo = typeof(PropertyInfoExtensionsTestClass)
                .GetProperties()
                .First(p => p.Name == propertyName);

            propertyInfo.SetProperty(serializedValue, exampleObject);

            var output = propertyInfo.GetValue(exampleObject).ToString();
            output.Should().Be(expectedStringValue);
        }

        [Theory]
        [InlineData("ExampleDateTime", "2019-abc-01T00:00:00")]
        [InlineData("ExampleEnum", "InvalidEnumType")]
        public void Invalidly_formatted_strings_throw_exception(string propertyName, string serializedValue)
        {
            var exampleObject = new PropertyInfoExtensionsTestClass();

            var propertyInfo = typeof(PropertyInfoExtensionsTestClass)
                .GetProperties()
                .First(p => p.Name == propertyName);

            propertyInfo.Invoking(p => p.SetProperty(serializedValue, exampleObject))
                .Should()
                .Throw<Exception>();
        }

        public class PropertyInfoExtensionsTestClass
        {
            public DateTime ExampleDateTime { get; set; }
            public PropertyInfoExtensionsTestEnum ExampleEnum { get; set; }
        }

        public enum PropertyInfoExtensionsTestEnum
        {
            ExampleA = 0,
            ExampleB = 1
        }
    }
}
