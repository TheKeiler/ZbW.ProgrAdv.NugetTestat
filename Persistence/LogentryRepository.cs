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

        public LogEntryRepository(string connectionString) : base(connectionString)
        {

        }

        public override LogEntry GetSingle<P>(P pkValue)
        {
            var logEntry = new LogEntry();
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TableName} WHERE Id = {pkValue}";
                IDataReader reader = cmd.ExecuteReader();

                object[] dataRow = new object[reader.FieldCount];
                int cols = reader.GetValues(dataRow); // tatsächliches Lesen 

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
            return logEntry;
        }

        public override void Add(LogEntry entity)
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

        public override List<LogEntry> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var logEntries = new List<LogEntry>();

            var whereCon = whereCondition;
            if (parameterValues.Count > 0 && whereCondition != null)
            {
                foreach (KeyValuePair<string, object> p in parameterValues)
                {
                    whereCon = whereCon.Replace($"@{p.Key}", p.Value.ToString());
                }
            }

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT id, pod, location, hostname, severity, timestamp, message FROM {TableName} WHERE {whereCon}";
                //----- SQL-Kommando ausführen; liefert einen OleDbDataReader
                IDataReader reader = cmd.ExecuteReader();


                object[] dataRow = new object[reader.FieldCount];
                //----- Daten zeilenweise lesen und verarbeiten
                while (reader.Read())
                { // solange noch Daten vorhanden sind
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
                    logEntries.Add(logEntry);
                }
                //----- Reader schließen
                reader.Close();
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
            return logEntries;
        }

        public override List<LogEntry> GetAll()
        {
            var logEntries = new List<LogEntry>();

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT id, pod, location, hostname, severity, timestamp, message FROM {TableName}";
                //----- SQL-Kommando ausführen; liefert einen OleDbDataReader
                IDataReader reader = cmd.ExecuteReader();


                object[] dataRow = new object[reader.FieldCount];
                //----- Daten zeilenweise lesen und verarbeiten
                while (reader.Read())
                { // solange noch Daten vorhanden sind
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
                    logEntries.Add(logEntry);
                }
                //----- Reader schließen
                reader.Close();
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
            return logEntries;
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
    }
}
