using RaGae.ModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PersonLibTest
{
    public class PersonTest
    {
        static string firstName = "FirstNameTest";
        static string lastName = "LastNameTest";

        public enum Constructor
        {
            WithoutArguments,
            WithArguments
        }

        public Person CreateConstructor(Constructor ctor, string firstName, string lastName)
        {
            switch (ctor)
            {
                case Constructor.WithoutArguments:
                    return new()
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };
                case Constructor.WithArguments:
                    return new(firstName, lastName);
                default:
                    throw new Exception("TILT: Should not be reached");
            }
        }

        [Fact]
        public void CreateConstructorWithEmpyData_Passing()
        {
            Person p = new();

            Assert.NotNull(p);
            Assert.Equal($" ", p.ToString());
        }

        public static IEnumerable<object[]> GetData_Passing()
        {
            foreach (Constructor ctor in Enum.GetValues(typeof(Constructor)).Cast<Constructor>().ToList())
            {
                yield return new object[] { ctor, firstName, lastName };
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Passing))]
        public void CreateConstructor_Passing(Constructor ctor, string firstName, string lastName)
        {
            Person p = this.CreateConstructor(ctor, firstName, lastName);

            Assert.Equal(firstName, p.FirstName);
            Assert.Equal(lastName, p.LastName);

            Assert.Equal($"{firstName} {lastName}", p.ToString());
        }

        public static IEnumerable<object[]> GetData_Failing()
        {
            foreach (Constructor ctor in Enum.GetValues(typeof(Constructor)).Cast<Constructor>().ToList())
            {
                yield return new object[] { ctor, null, lastName, $"{nameof(Person)}:{nameof(Person.FirstName)}" };
                yield return new object[] { ctor, firstName, null, $"{nameof(Person)}:{nameof(Person.LastName)}" };
                yield return new object[] { ctor, string.Empty, lastName, $"{nameof(Person)}:{nameof(Person.FirstName)}" };
                yield return new object[] { ctor, firstName, string.Empty, $"{nameof(Person)}:{nameof(Person.LastName)}" };
                yield return new object[] { ctor, " ", lastName, $"{nameof(Person)}:{nameof(Person.FirstName)}" };
                yield return new object[] { ctor, firstName, " ", $"{nameof(Person)}:{nameof(Person.LastName)}" };
            }
        }

        [Theory]
        [MemberData(nameof(GetData_Failing))]
        public void CreateConstructor_Failing(Constructor ctor, string firstName, string lastName, string message)
        {
            Person p;

            Exception ex = Assert.Throws<Exception>(() => p = this.CreateConstructor(ctor, firstName, lastName));

            Assert.Equal(message, ex.Message);
        }

        [Theory]
        [MemberData(nameof(GetData_Passing))]
        public void CreateConstructorAndClone_Passing(Constructor ctor, string firstName, string lastName)
        {
            Person p1 = this.CreateConstructor(ctor, firstName, lastName);
            Person p2 = (Person)p1.Clone();

            p1.FirstName += (new Random()).Next(0, 10);
            p1.LastName += (new Random()).Next(0, 10);

            Assert.NotEqual(p1, p2);

            Assert.Equal(firstName, p2.FirstName);
            Assert.Equal(lastName, p2.LastName);

            Assert.Equal($"{firstName} {lastName}", p2.ToString());
        }
    }
}
