using LinqToDB.Data;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class LogEntryRepository : RepositoryBase<LogEntry>
    {
        public LogEntryRepository() : base()
        {
        }

        public new void Add(LogEntry entity)
        {
            throw new System.NotSupportedException();
        }

        public override void Delete(LogEntry entity)
        {
            throw new System.NotSupportedException();
        }

        public override void Update(LogEntry entity)
        {
            throw new System.NotSupportedException();
        }

        public void ExecuteLogClear(LogEntry entry)
        {
            using (var db = new DataConnection(ProviderName))
            {
                db.QueryProc<LogEntry>("LogClear", new DataParameter("@_logentries_id", entry.Id));
            }

        }

        public void ExecuteLogMessageAdd(LogEntry newEntry)
        {
            using (var db = new DataConnection(ProviderName))
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
