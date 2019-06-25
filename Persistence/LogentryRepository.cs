using LinqToDB.Data;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class LogEntryRepository : RepositoryBase<LogEntry>
    {
        public LogEntryRepository(string connectionString) : base(connectionString)
        {
        }

        public new void Add(LogEntry entity)
        {
            throw new System.NotSupportedException();
        }

        public new void Delete(LogEntry entity)
        {
            throw new System.NotSupportedException();
        }

        public new void Update(LogEntry entity)
        {
            throw new System.NotSupportedException();
        }

        public void ExecuteLogClear(LogEntry entry)
        {
            using (var db = new DataConnection(ProviderName, ConnectionString))
            {
                db.QueryProc<LogEntry>("LogClear", new DataParameter("@_logentries_id", entry.Id));
            }

        }

        public void ExecuteLogMessageAdd(LogEntry newEntry)
        {
            using (var db = new DataConnection(ProviderName, ConnectionString))
            {
                var dataParams = new DataParameter[4];
                dataParams[0] = new DataParameter("@i_pod", newEntry.Id);
                dataParams[1] = new DataParameter("@i_hostname", newEntry.Hostname);
                dataParams[2] = new DataParameter("@i_severity", newEntry.Severity);
                dataParams[3] = new DataParameter("@i_message", newEntry.Message);
                db.QueryProc<LogEntry>("LogMessageAdd", dataParams);
            }
        }
    }
}
