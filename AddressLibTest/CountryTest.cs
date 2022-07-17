using RaGae.ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AddressLibTest
{
    public class CountryTest
    {
        static string countryCode = "CountryCodeTest";
        static string countryName = "CountryNameTest";

        public enum Constructor
        {
            WithoutArguments,
            WithArguments
        }

        public Country CreateConstructor(Constructor ctor, string countryCode, string countryName)
        {
            switch (ctor)
            {
                case Constructor.WithoutArguments:
                    return new()
                    {
                        Code = countryCode,
                        Name = countryName
                    };
                case Constructor.WithArguments:
                    return new(countryCode, countryName);
                default:
                    throw new Exception("TILT: Should not be reached");
            }
        }

        [Fact]
        public void CreateConstructorWithEmpyData_Passing()
        {
            Country c = new();

            Assert.NotNull(c);
            Assert.Equal($"-", c.ToString());
        }

        public static IEnumerable<object[]> GetData_Passing()
        {
            foreach (Constructor ctor in Enum.GetValues(typeof(Constructor)).Cast<Constructor>().ToList())
            {
                yield return new object[] { ctor, countryCode, countryName };
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Passing))]
        public void CreateConstructor_Passing(Constructor ctor, string countryCode, string countryName)
        {
            Country c = this.CreateConstructor(ctor, countryCode, countryName);

            Assert.Equal(countryCode, c.Code);
            Assert.Equal(countryName, c.Name);

            Assert.Equal($"{countryCode}-{countryName}", c.ToString());
        }

        public static IEnumerable<object[]> GetData_Failing()
        {
            foreach (Constructor ctor in Enum.GetValues(typeof(Constructor)).Cast<Constructor>().ToList())
            {
                yield return new object[] { ctor, null, countryName, $"{nameof(Country)}:{nameof(Country.Code)}" };
                yield return new object[] { ctor, countryCode, null, $"{nameof(Country)}:{nameof(Country.Name)}" };
                yield return new object[] { ctor, string.Empty, countryName, $"{nameof(Country)}:{nameof(Country.Code)}" };
                yield return new object[] { ctor, countryCode, string.Empty, $"{nameof(Country)}:{nameof(Country.Name)}" };
                yield return new object[] { ctor, " ", countryName, $"{nameof(Country)}:{nameof(Country.Code)}" };
                yield return new object[] { ctor, countryCode, " ", $"{nameof(Country)}:{nameof(Country.Name)}" };
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Failing))]
        public void CreateConstructor_Failing(Constructor ctor, string countryCode, string countryName, string message)
        {
            Country c;

            Exception ex = Assert.Throws<Exception>(() => c = this.CreateConstructor(ctor, countryCode, countryName));

            Assert.Equal(message, ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetData_Passing))]
        public void CreateConstructorAndClone_Passing(Constructor ctor, string countryCode, string countryName)
        {
            Country c1 = this.CreateConstructor(ctor, countryCode, countryName);
            Country c2 = (Country)c1.Clone();

            c1.Code += (new Random()).Next(0, 10);
            c1.Name += (new Random()).Next(0, 10);

            Assert.NotEqual(c1, c2);

            Assert.Equal(countryCode, c2.Code);
            Assert.Equal(countryName, c2.Name);

            Assert.Equal($"{countryCode}-{countryName}", c2.ToString());
        }
    }
}
