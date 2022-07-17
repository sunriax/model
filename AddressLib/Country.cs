using System;

namespace RaGae.ModelLib
{
    public class Country : ICloneable
    {
        private string code;
        private string name;

        public Country() { }

        public Country(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

        public string Code
        {
            get => this.code;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(Country)}:{nameof(Code)}");

                this.code = value;
            }
        }

        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception($"{nameof(Country)}:{nameof(Name)}");

                this.name = value;
            }
        }

        public object Clone() => this.MemberwiseClone();

        public override string ToString() => $"{this.Code}-{this.Name}";
    }
}
