using LinqToDB.Mapping;
using System;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    [Table("v_logentries")]
    public class LogEntry : ModelBase
    {
        [Column("id"), PrimaryKey, NotNull]
        public override int Id { get; set; }
        [Column("pod")]
        public string Pod { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("hostname")]
        public string Hostname { get; set; }
        [Column("severity")]
        public int Severity { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        [Column("message")]
        public string Message { get; set; }
        public bool IsDuplicate { get; set; }

        public override bool Equals(object value)
        {
            return Equals(value as LogEntry);
        }

        public bool Equals(LogEntry logEntry)
        {
            if (Object.ReferenceEquals(null, logEntry)) return false;
            if (Object.ReferenceEquals(this, logEntry)) return true;

            return String.Equals(Severity, logEntry.Severity)
                   && string.Equals(Message, logEntry.Message);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Large primes to avoid hashing collisions
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, Severity) ?
                           Severity.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!Object.ReferenceEquals(null, Message) ?
                           Message.GetHashCode() : 0);
                return hash;
            }
        }

        public static bool operator ==(LogEntry logA, LogEntry logB)
        {
            if (Object.ReferenceEquals(logA, logB))
            {
                return true;
            }

            //Ensure that A isnt Null
            if (Object.ReferenceEquals(null, logA))
            {
                return false;
            }

            return (logA.Equals(logB));
        }

        public static bool operator !=(LogEntry logA, LogEntry logB)
        {
            return !(logA == logB);
        }
    }

}
