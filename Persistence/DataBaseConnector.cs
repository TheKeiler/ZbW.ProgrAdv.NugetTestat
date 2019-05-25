using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class DataBaseConnector
    {
        // Connectionstring: https://www.connectionstrings.com/mysql/ 
        private readonly string _connectionString;
        public DataBaseConnector(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public ObservableCollection<LogEntry> Read()
        {
            
            string connStr = this._connectionString;

            ObservableCollection<LogEntry> logEntries = new ObservableCollection<LogEntry>();

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(connStr);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT id, pod, location, hostname, severity, timestamp, message FROM v_logentries";
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
                MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: "+e.Message);
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


        public void LogClear(LogEntry entry)
        {
            var connStr = this._connectionString;
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(connStr);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = "CALL LogClear(" + entry.Id + ");";
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

        public void LogMessageAdd(LogEntry newEntry)
        {
            string connStr = this._connectionString;
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(connStr);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = "CALL LogMessageAdd('" + newEntry.Pod +"', '"+newEntry.Hostname+"', '"+newEntry.Severity+"', '"+newEntry.Message+"');";
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
