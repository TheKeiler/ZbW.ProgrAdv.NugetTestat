using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    //TODO: Refactor Class with LINQ
    public class LocationRepository : RepositoryBase<Location>
    {
        public override string TableName => "location";

        public LocationRepository(string connectionString) : base(connectionString)
        {
        }

        public override void Add(Location location)
        {
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    $"INSERT INTO {TableName} (parent_location , address_fk , designation , building , room)" +
                    $"VALUE" +
                    $"({location.ParentId},{location.AddressId},{location.Designation},{location.BuildingNr},{location.RoomNr})";
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

        public override void Delete(Location location)
        {
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"DELETE FROM {TableName} WHERE location_id = {location.Id}";
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

        public override List<Location> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var whereCon = whereCondition;
            if (parameterValues.Count > 0 && whereCondition != null)
            {
                foreach (KeyValuePair<string, object> p in parameterValues)
                {
                    whereCon = whereCon.Replace($"@{p.Key}", p.Value.ToString());
                }
            }

            var locationList = new List<Location>();

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT location_id, parent_location, address_fk, designation, building, room FROM {TableName} WHERE {whereCon}";
                //----- SQL-Kommando ausführen; liefert einen OleDbDataReader
                IDataReader reader = cmd.ExecuteReader();

                object[] dataRow = new object[reader.FieldCount];
                //----- Daten zeilenweise lesen und verarbeiten
                while (reader.Read())
                { // solange noch Daten vorhanden sind
                    int cols = reader.GetValues(dataRow); // tatsächliches Lesen 

                    var location = new Location();
                    for (int i = 0; i < cols; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                location.Id = Convert.ToInt32(dataRow[i]);
                                break;
                            case 1:
                                if (reader.IsDBNull(1))
                                {
                                    location.ParentId = null;
                                }
                                else
                                {
                                    location.ParentId = Convert.ToInt32(dataRow[i]);
                                }
                                break;
                            case 2:
                                location.AddressId = Convert.ToInt32(dataRow[i]);
                                break;
                            case 3:
                                location.Designation = dataRow[i].ToString();
                                break;
                            case 4:
                                location.BuildingNr = Convert.ToInt32(dataRow[i]);
                                break;
                            case 5:
                                location.RoomNr = Convert.ToInt32(dataRow[i]);
                                break;
                            default:
                                Console.WriteLine("Da kahmen zu viele Felder");
                                break;
                        }
                    }
                    locationList.Add(location);
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
            return locationList;
        }

        public override List<Location> GetAll()
        {
            var locationList = new List<Location>();

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT location_id, parent_location, address_fk, designation, building, room FROM {TableName}";
                //----- SQL-Kommando ausführen; liefert einen OleDbDataReader
                IDataReader reader = cmd.ExecuteReader();

                object[] dataRow = new object[reader.FieldCount];
                //----- Daten zeilenweise lesen und verarbeiten
                while (reader.Read())
                { // solange noch Daten vorhanden sind
                    int cols = reader.GetValues(dataRow); // tatsächliches Lesen 

                    var location = new Location();
                    for (int i = 0; i < cols; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                location.Id = Convert.ToInt32(dataRow[i]);
                                break;
                            case 1:
                                if (reader.IsDBNull(1))
                                {
                                    location.ParentId = null;
                                }
                                else
                                {
                                    location.ParentId = Convert.ToInt32(dataRow[i]);
                                }
                                break;
                            case 2:
                                location.AddressId = Convert.ToInt32(dataRow[i]);
                                break;
                            case 3:
                                location.Designation = dataRow[i].ToString();
                                break;
                            case 4:
                                location.BuildingNr = Convert.ToInt32(dataRow[i]);
                                break;
                            case 5:
                                location.RoomNr = Convert.ToInt32(dataRow[i]);
                                break;
                            default:
                                Console.WriteLine("Da kahmen zu viele Felder");
                                break;
                        }
                    }
                    locationList.Add(location);
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
            return locationList;
        }

        public override Location GetSingle<P>(P pkValue)
        {
            var location = new Location();

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT location_id, parent_location, address_fk, designation, building, room FROM {TableName} WHERE location_id = {pkValue}";
                //----- SQL-Kommando ausführen; liefert einen OleDbDataReader
                IDataReader reader = cmd.ExecuteReader();

                object[] dataRow = new object[reader.FieldCount];
                int cols = reader.GetValues(dataRow); // tatsächliches Lesen 

                for (int i = 0; i < cols; i++)
                {
                    switch (i)
                    {
                        case 0:
                            location.Id = Convert.ToInt32(dataRow[i]);
                            break;
                        case 1:
                            if (reader.IsDBNull(1))
                            {
                                location.ParentId = null;
                            }
                            else
                            {
                                location.ParentId = Convert.ToInt32(dataRow[i]);
                            }
                            break;
                        case 2:
                            location.AddressId = Convert.ToInt32(dataRow[i]);
                            break;
                        case 3:
                            location.Designation = dataRow[i].ToString();
                            break;
                        case 4:
                            location.BuildingNr = Convert.ToInt32(dataRow[i]);
                            break;
                        case 5:
                            location.RoomNr = Convert.ToInt32(dataRow[i]);
                            break;
                        default:
                            Console.WriteLine("Da kahmen zu viele Felder");
                            break;
                    }
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
            return location;
        }

        public override void Update(Location location)
        {
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE {TableName} SET " +
                    $"parent_location = {location.ParentId}, address_fk = {location.AddressId} , designation = {location.Designation}, building = {location.BuildingNr} , room = {location.RoomNr} " +
                    $"WHERE location_id = {location.Id}";
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

        public Node<Location> GenerateLocationTreeFromList(List<Location> locationList)
        {
            var treeBuilder = new LocationTreeBuilder();
            var locationNode = treeBuilder.BuildTree(locationList);
            return locationNode;
        }
    }
}
