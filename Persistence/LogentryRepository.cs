using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class LogEntryRepository : RepositoryBase<LogEntry>
    {
        public override string TableName => "v_logentries";

        public override string TablePrimaryKey => "id";

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

        //TODO: Refactor with Linq
        public void ExecuteLogClear(LogEntry entry)
        {

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"CALL LogClear({entry.Id});";
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
            }
            finally
            {
                try
                {
                    if (con != null)
                        // Verbindung schließen
                        con.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        //TODO: Refactor with Linq
        public void ExecuteLogMessageAdd(LogEntry newEntry)
        {
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"CALL LogMessageAdd('{ newEntry.Pod}', '{newEntry.Hostname}', '{newEntry.Severity}', '{newEntry.Message}');";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
            }
            finally
            {
                try
                {
                    if (con != null)
                        // Verbindung schließen
                        con.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
    }
}
