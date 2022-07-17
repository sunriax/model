using System;

namespace RaGae.ModelLib
{
    public class Address : ICloneable
    {
        private string street;
        private string number;

        public Address() { }

        public Address(string street, string number)
        {
            this.Street = street;
            this.Number = number;
        }

        public string Street
        {
            get => this.street;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(Address)}:{nameof(Street)}");

                this.street = value;
            }
        }

        public string Number
        {
            get => this.number;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(Address)}:{nameof(Number)}");

                this.number = value;
            }
        }

        public City City { get; set; }

        public Country Country { get; set; }

        public object Clone()
        {
            Address address = (Address)this.MemberwiseClone();
            address.City = (City)this.City.Clone();
            address.Country = (Country)this.Country.Clone();

            return address;
        }

        public override string ToString() => $"{this.Street} {this.Number}";
    }
}
