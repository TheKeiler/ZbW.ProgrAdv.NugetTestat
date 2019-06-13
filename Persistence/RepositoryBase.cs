using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    //TODO: More Generic Methods possible with LINQ
    public abstract class RepositoryBase<M> : IRepositoryBase<M> where M : IModelBase, new()
    {
        protected string ConnectionString { get; }
        public abstract string TableName { get; }
        public abstract string FieldsInSelectStatement { get; }
        public abstract string TablePrimaryKey { get; }
        public abstract string FieldsInAddStatement { get; }

        protected RepositoryBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public M GetSingle<P>(P pkValue)
        {
            M entity = new M();

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT {FieldsInSelectStatement} FROM {TableName} WHERE {TablePrimaryKey} = {pkValue}";
                //----- SQL-Kommando ausführen; liefert einen OleDbDataReader
                IDataReader reader = cmd.ExecuteReader();
                var entityList = GenerateEntityListFromReader(reader);
                entity = entityList[0];
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
            return entity;
        }

        public void Add(M entity)
        {
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    $"INSERT INTO {TableName} ({FieldsInAddStatement})" +
                    $"VALUE" +
                    $"({GenerateAddStatementValues(entity)})";
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

        public abstract string GenerateAddStatementValues(M entity);
        public abstract string GenerateUpdateStatementValues(M entity);

        public void Delete(M entity)
        {
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"DELETE FROM {TableName} WHERE {TablePrimaryKey} = {entity.Id}";
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

        public void Update(M entity)
        {
            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE {TableName} SET {GenerateUpdateStatementValues(entity)}";
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

        public List<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var whereCon = whereCondition;
            if (parameterValues.Count > 0 && whereCondition != null)
            {
                foreach (KeyValuePair<string, object> p in parameterValues)
                {
                    whereCon = whereCon.Replace($"@{p.Key}", p.Value.ToString());
                }
            }

            var entityList = new List<M>();

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT {FieldsInSelectStatement} FROM {TableName} WHERE {whereCon}";
                //----- SQL-Kommando ausführen; liefert einen OleDbDataReader
                IDataReader reader = cmd.ExecuteReader();

                entityList = GenerateEntityListFromReader(reader);
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
            return entityList;
        }

        public List<M> GetAll()
        {
            var entityList = new List<M>();

            IDbConnection con = null;       // Verbindung deklarieren
            try
            {
                con = new MySqlConnection(ConnectionString);   //Verbindung erzeugen
                con.Open();
                //----- SQL-Kommando aufbauen
                IDbCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT {FieldsInSelectStatement} FROM {TableName}";
                //----- SQL-Kommando ausführen; liefert einen OleDbDataReader
                IDataReader reader = cmd.ExecuteReader();

                entityList = GenerateEntityListFromReader(reader);
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
            return entityList;
        }

        public long Count(string whereCondition, Dictionary<string, object> parameterValues)
        {
            var whereCon = whereCondition;
            if (parameterValues.Count > 0 && whereCondition != null)
            {
                foreach (KeyValuePair<string, object> p in parameterValues)
                {
                    whereCon = whereCon.Replace($"@{p.Key}", p.Value.ToString());
                }
            }

            using (var conn = new MySqlConnection(this.ConnectionString))
            {
                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = $"select count(*) from {this.TableName} where {whereCon}";
                        return (long)cmd.ExecuteScalar();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                    return -1;
                }
                finally
                {
                    try
                    {
                        if (conn != null)
                            // Verbindung schließen
                            conn.Close();
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
        }
        public long Count()
        {
            using (var conn = new MySqlConnection(this.ConnectionString))
            {
                try
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = $"select count(*) from {this.TableName}";
                        return (long)cmd.ExecuteScalar();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                    return -1;
                }
                finally
                {
                    try
                    {
                        if (conn != null)
                            // Verbindung schließen
                            conn.Close();
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
        }

        public abstract List<M> GenerateEntityListFromReader(IDataReader reader);
    }
}
