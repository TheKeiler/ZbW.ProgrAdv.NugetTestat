using LinqToDB.Mapping;
using System;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    [Table("Customer")]
    public class Customer : ModelBase
    {
        [Column("customer_id"), PrimaryKey, NotNull]
        public override int Id { get; set; }
        [Column("firstname"), NotNull]
        public string Firstname { get; set; }
        [Column("lastname"), NotNull]
        public string Lastname { get; set; }
        [Column("customernumber"), NotNull]
        public string CustomerNumber { get; set; }
        [Column("tel")]
        public string PhoneNumber { get; set; }
        [Column("eMail")]
        public string EMail { get; set; }
        [Column("url")]
        public string Url { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("kundenkonto_fk")]
        public int AccountNummber { get; set; }
        public Country CustomerCountry { get; set; } 

        public void SetAccountNummber()
        {
            Random rnd = new Random();
            AccountNummber = rnd.Next(1, 9);
        }

        public override bool Equals(object value)
        {
            return Equals(value as Customer);
        }

        public bool Equals(Customer Customer)
        {
            if (Object.ReferenceEquals(null, Customer)) return false;
            if (Object.ReferenceEquals(this, Customer)) return true;

            return string.Equals(CustomerNumber, Customer.CustomerNumber)
                   && string.Equals(Lastname, Customer.Lastname)
                   && string.Equals(Firstname, Customer.Firstname);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, CustomerNumber) ?
                           CustomerNumber.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, Lastname) ?
                           Lastname.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, Firstname) ?
                           Firstname.GetHashCode() : 0);
                return hash;
            }
        }

        public static bool operator ==(Customer locA, Customer locB)
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

        public static bool operator !=(Customer locA, Customer locB)
        {
            return !(locA == locB);
        }

        public override string ToString()
        {
            return string.Concat(CustomerNumber, "-", Firstname, "-", Lastname);
        }
    }
}
