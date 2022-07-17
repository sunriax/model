using RaGae.ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AddressLibTest
{
    public class CityTest
    {
        static int cityZip = 1337;
        static string cityName = "CityNameTest";

        public enum Constructor
        {
            WithoutArguments,
            WithArguments
        }

        public City CreateConstructor(Constructor ctor, int cityZip, string cityName)
        {
            switch (ctor)
            {
                case Constructor.WithoutArguments:
                    return new()
                    {
                        Zip = cityZip,
                        Name = cityName
                    };
                case Constructor.WithArguments:
                    return new(cityZip, cityName);
                default:
                    throw new Exception("TILT: Should not be reached");
            }
        }

        [Fact]
        public void CreateConstructorWithEmpyData_Passing()
        {
            City c = new();

            Assert.NotNull(c);
            Assert.Equal($"0 ", c.ToString());
        }

        public static IEnumerable<object[]> GetData_Passing()
        {
            foreach (Constructor ctor in Enum.GetValues(typeof(Constructor)).Cast<Constructor>().ToList())
            {
                yield return new object[] { ctor, cityZip, cityName };
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Passing))]
        public void CreateConstructor_Passing(Constructor ctor, int cityZip, string cityName)
        {
            City c = this.CreateConstructor(ctor, cityZip, cityName);

            Assert.Equal(cityZip, c.Zip);
            Assert.Equal(cityName, c.Name);

            Assert.Equal($"{cityZip} {cityName}", c.ToString());
        }

        public static IEnumerable<object[]> GetData_Failing()
        {
            foreach (Constructor ctor in Enum.GetValues(typeof(Constructor)).Cast<Constructor>().ToList())
            {
                yield return new object[] { ctor, -1, cityName, $"{nameof(City)}:{nameof(City.Zip)}" };
                yield return new object[] { ctor, cityZip, null, $"{nameof(City)}:{nameof(City.Name)}" };
                yield return new object[] { ctor, -2, cityName, $"{nameof(City)}:{nameof(City.Zip)}" };
                yield return new object[] { ctor, cityZip, string.Empty, $"{nameof(City)}:{nameof(City.Name)}" };
                yield return new object[] { ctor, int.MinValue, cityName, $"{nameof(City)}:{nameof(City.Zip)}" };
                yield return new object[] { ctor, cityZip, " ", $"{nameof(City)}:{nameof(City.Name)}" };
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Failing))]
        public void CreateConstructor_Failing(Constructor ctor, int cityZip, string cityName, string message)
        {
            City c;

            Exception ex = Assert.Throws<Exception>(() => c = this.CreateConstructor(ctor, cityZip, cityName));

            Assert.Equal(message, ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetData_Passing))]
        public void CreateConstructorAndClone_Passing(Constructor ctor, int cityZip, string cityName)
        {
            City c1 = this.CreateConstructor(ctor, cityZip, cityName);
            City c2 = (City)c1.Clone();

            c1.Zip += (new Random()).Next(0, 10);
            c1.Name += (new Random()).Next(0, 10);

            Assert.NotEqual(c1, c2);

            Assert.Equal(cityZip, c2.Zip);
            Assert.Equal(cityName, c2.Name);

            Assert.Equal($"{cityZip} {cityName}", c2.ToString());
        }
    }
}
