using System;

namespace RaGae.ModelLib
{
    public class Person : ICloneable
    {
        private string firstName;
        private string lastName;

        public Person() { }

        public Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName
        {
            get => this.firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(Person)}:{nameof(FirstName)}");

                this.firstName = value;
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(Person)}:{nameof(LastName)}");

                this.lastName = value;
            }
        }

        public object Clone() => this.MemberwiseClone();

        public override string ToString() => $"{this.FirstName} {this.LastName}";
    }
}
