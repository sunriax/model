using RaGae.ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AddressLibTest
{
    public class AddressTest
    {
        static string street = "StreetTest";
        static string number = "NumberTest";
        static int cityZip = 1337;
        static string cityName = "CityTest";
        static string countryCode = "CodeTest";
        static string countryName = "CountryTest";

        public enum Constructor
        {
            WithoutArguments,
            WithArguments
        }

        public Address CreateConstructor(Constructor ctor, string street, string number, int cityZip, string cityName, string countryCode, string countryName)
        {
            switch (ctor)
            {
                case Constructor.WithoutArguments:
                    return new()
                    {
                        Street = street,
                        Number = number,
                        City = new City
                        {
                            Zip = cityZip,
                            Name = cityName
                        },
                        Country = new Country
                        {
                            Code = countryCode,
                            Name = countryName
                        }

                    };
                case Constructor.WithArguments:
                    return new(street, number)
                    {
                        City = new City(cityZip, cityName),
                        Country = new Country(countryCode, countryName)
                    };
                default:
                    throw new Exception("TILT: Should not be reached");
            }
        }

        [Fact]
        public void CreateConstructorWithEmpyData_Passing()
        {
            Address a = new();

            Assert.NotNull(a);
            Assert.Null(a.City);
            Assert.Null(a.Country);
            Assert.Equal($" ", a.ToString());
        }

        public static IEnumerable<object[]> GetData_Passing()
        {
            foreach (Constructor ctor in Enum.GetValues(typeof(Constructor)).Cast<Constructor>().ToList())
            {
                yield return new object[] { ctor, street, number, cityZip, cityName, countryCode, countryName };
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Passing))]
        public void CreateConstructor_Passing(Constructor ctor, string street, string number, int cityZip, string cityName, string countryCode, string countryName)
        {
            Address a = this.CreateConstructor(ctor, street, number, cityZip, cityName, countryCode, countryName);

            Assert.Equal(street, a.Street);
            Assert.Equal(number, a.Number);

            Assert.NotNull(a.City);
            Assert.NotNull(a.Country);

            Assert.Equal($"{street} {number}", a.ToString());
        }

        public static IEnumerable<object[]> GetData_Failing()
        {
            foreach (Constructor ctor in Enum.GetValues(typeof(Constructor)).Cast<Constructor>().ToList())
            {
                yield return new object[] { ctor, null, number, cityZip, cityName, countryCode, countryName, $"{nameof(Address)}:{nameof(Address.Street)}" };
                yield return new object[] { ctor, street, null, cityZip, cityName, countryCode, countryName, $"{nameof(Address)}:{nameof(Address.Number)}" };
                yield return new object[] { ctor, street, number, -1, cityName, countryCode, countryName, $"{nameof(City)}:{nameof(City.Zip)}" };
                yield return new object[] { ctor, street, number, cityZip, null, countryCode, countryName, $"{nameof(City)}:{nameof(City.Name)}" };
                yield return new object[] { ctor, street, number, cityZip, cityName, null, countryName, $"{nameof(Country)}:{nameof(Country.Code)}" };
                yield return new object[] { ctor, street, number, cityZip, cityName, countryCode, null, $"{nameof(Country)}:{nameof(Country.Name)}" };

                yield return new object[] { ctor, string.Empty, number, cityZip, cityName, countryCode, countryName, $"{nameof(Address)}:{nameof(Address.Street)}" };
                yield return new object[] { ctor, street, string.Empty, cityZip, cityName, countryCode, countryName, $"{nameof(Address)}:{nameof(Address.Number)}" };
                yield return new object[] { ctor, street, number, -2, cityName, countryCode, countryName, $"{nameof(City)}:{nameof(City.Zip)}" };
                yield return new object[] { ctor, street, number, cityZip, string.Empty, countryCode, countryName, $"{nameof(City)}:{nameof(City.Name)}" };
                yield return new object[] { ctor, street, number, cityZip, cityName, string.Empty, countryName, $"{nameof(Country)}:{nameof(Country.Code)}" };
                yield return new object[] { ctor, street, number, cityZip, cityName, countryCode, string.Empty, $"{nameof(Country)}:{nameof(Country.Name)}" };

                yield return new object[] { ctor, " ", number, cityZip, cityName, countryCode, countryName, $"{nameof(Address)}:{nameof(Address.Street)}" };
                yield return new object[] { ctor, street, " ", cityZip, cityName, countryCode, countryName, $"{nameof(Address)}:{nameof(Address.Number)}" };
                yield return new object[] { ctor, street, number, int.MinValue, cityName, countryCode, countryName, $"{nameof(City)}:{nameof(City.Zip)}" };
                yield return new object[] { ctor, street, number, cityZip, " ", countryCode, countryName, $"{nameof(City)}:{nameof(City.Name)}" };
                yield return new object[] { ctor, street, number, cityZip, cityName, " ", countryName, $"{nameof(Country)}:{nameof(Country.Code)}" };
                yield return new object[] { ctor, street, number, cityZip, cityName, countryCode, " ", $"{nameof(Country)}:{nameof(Country.Name)}" };
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Failing))]
        public void CreateConstructor_Failing(Constructor ctor, string street, string number, int cityZip, string cityName, string countryCode, string countryName, string message)
        {
            Address a;

            Exception ex = Assert.Throws<Exception>(() => a = this.CreateConstructor(ctor, street, number, cityZip, cityName, countryCode, countryName));

            Assert.Equal(message, ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetData_Passing))]
        public void CreateConstructorAndClone_Passing(Constructor ctor, string street, string number, int cityZip, string cityName, string countryCode, string countryName)
        {
            Address a1 = this.CreateConstructor(ctor, street, number, cityZip, cityName, countryCode, countryName);
            Address a2 = (Address)a1.Clone();

            a1.Street += (new Random()).Next(0, 10);
            a1.Number += (new Random()).Next(0, 10);

            Assert.NotEqual(a1, a2);

            Assert.Equal(street, a2.Street);
            Assert.Equal(number, a2.Number);

            Assert.Equal($"{street} {number}", a2.ToString());
        }
    }
}
