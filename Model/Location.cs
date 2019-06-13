using System;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    public class Location : IModelBase
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int AddressId { get; set; }
        public string Designation { get; set; }
        public int BuildingNr { get; set; }
        public int RoomNr { get; set; }

        public override bool Equals(object value)
        {
            return Equals(value as Location);
        }

        public bool Equals(Location location)
        {
            if (Object.ReferenceEquals(null, location)) return false;
            if (Object.ReferenceEquals(this, location)) return true;

            return string.Equals(Designation, location.Designation)
                   && string.Equals(BuildingNr, location.BuildingNr)
                   && string.Equals(RoomNr, location.RoomNr);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, Designation) ?
                           Designation.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, BuildingNr) ?
                           BuildingNr.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, RoomNr) ?
                           RoomNr.GetHashCode() : 0);
                return hash;
            }
        }

        public static bool operator ==(Location locA, Location locB)
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

        public static bool operator !=(Location locA, Location locB)
        {
            return !(locA == locB);
        }

        public override string ToString()
        {
            return Designation;
        }
    }
}
