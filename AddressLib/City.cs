using System;

namespace RaGae.ModelLib
{
    public class City : ICloneable
    {
        private int zip;
        private string name;

        public City() { }

        public City(int zip, string name)
        {
            this.Zip = zip;
            this.Name = name;
        }

        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(City)}:{nameof(Name)}");

                this.name = value;
            }
        }

        public int Zip
        {
            get => this.zip;
            set
            {
                if (value < 0)
                    throw new Exception($"{nameof(City)}:{nameof(Zip)}");

                this.zip = value;
            }
        }

        public object Clone() => this.MemberwiseClone();

        public override string ToString() => $"{this.Zip} {this.Name}";
    }
}
