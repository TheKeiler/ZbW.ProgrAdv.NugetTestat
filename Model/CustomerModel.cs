using System;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    public class CustomerModel
    {
        public customer CostumerEntity { get; set; }

        public Country CustomerCountry { get; set; }

        public CustomerModel()
        {
            this.CostumerEntity = new customer();
            this.CustomerCountry = new Country("Schweiz", @"^(0041|0|\+41)(\(0\))?([0-9]{2}\/?[0-9]{7})$");
        }

        public CustomerModel(customer c)
        {
            this.CostumerEntity = c;
            this.CustomerCountry = new Country("Schweiz", @"^(0041|0|\+41)(\(0\))?([0-9]{2}\/?[0-9]{7})$");
        }

        public void SetAccountNummber()
        {
            Random rnd = new Random();
            CostumerEntity.kundenkonto_fk = rnd.Next(1, 9);
        }

        public override bool Equals(object value)
        {
            return Equals(value as CustomerModel);
        }

        public bool Equals(CustomerModel Customer)
        {
            if (Object.ReferenceEquals(null, Customer)) return false;
            if (Object.ReferenceEquals(this, Customer)) return true;

            return string.Equals(this.CostumerEntity.customernumber, Customer.CostumerEntity.customernumber)
                   && string.Equals(this.CostumerEntity.lastname, Customer.CostumerEntity.lastname)
                   && string.Equals(this.CostumerEntity.firstname, Customer.CostumerEntity.firstname);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, CostumerEntity.customernumber) ?
                           CostumerEntity.customernumber.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, CostumerEntity.lastname) ?
                           CostumerEntity.lastname.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, CostumerEntity.firstname) ?
                           CostumerEntity.firstname.GetHashCode() : 0);
                return hash;
            }
        }

        public static bool operator ==(CustomerModel locA, CustomerModel locB)
        {
            if (Object.ReferenceEquals(locA, locB))
            {
                return true;
            }

            //Ensure that A isnt Null
            if (Object.ReferenceEquals(null, locA))
            {
                return false;
            }

            return (locA.Equals(locB));
        }

        public static bool operator !=(CustomerModel locA, CustomerModel locB)
        {
            return !(locA == locB);
        }

        public override string ToString()
        {
            return string.Concat(CostumerEntity.customernumber, "-", CostumerEntity.firstname, "-", CostumerEntity.lastname);
        }
    }
}
