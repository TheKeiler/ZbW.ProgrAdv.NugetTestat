using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class LogEntryRepository : RepositoryBase<LogEntry>
    {
        public override string TableName => "v_logentries";

        public override string FieldsInSelectStatement => "id, pod, location, hostname, severity, timestamp, message";

        public override string TablePrimaryKey => "id";

        public override string FieldsInAddStatement => throw new NotSupportedException();

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

        public override string GenerateAddStatementValues(LogEntry entity)
        {
            throw new NotSupportedException();
        }

        public override string GenerateUpdateStatementValues(LogEntry entity)
        {
            throw new NotSupportedException();
        }

        public override List<LogEntry> GenerateEntityListFromReader(IDataReader reader)
        {
            var logentryList = new List<LogEntry>();
            object[] dataRow = new object[reader.FieldCount];
            
            while (reader.Read())
            {
                // solange noch Daten vorhanden sind
                int cols = reader.GetValues(dataRow); // tatsächliches Lesen 

                var logEntry = new LogEntry();
                for (int i = 0; i < cols; i++)
                {
                    switch (i)
                    {
                        case 0:
                            logEntry.Id = (int)dataRow[i];
                            break;
                        case 1:
                            logEntry.Pod = dataRow[i].ToString();
                            break;
                        case 2:
                            logEntry.Location = dataRow[i].ToString();
                            break;
                        case 3:
                            logEntry.Hostname = dataRow[i].ToString();
                            break;
                        case 4:
                            logEntry.Severity = (int)dataRow[i];
                            break;
                        case 5:
                            logEntry.Timestamp = (DateTime)dataRow[i];
                            break;
                        case 6:
                            logEntry.Message = dataRow[i].ToString();
                            break;
                        default:
                            Console.WriteLine("Da kahmen zu viele Felder");
                            break;
                    }
                }
                logentryList.Add(logEntry);
            }
            return logentryList;
        }
    }
}
