using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    //TODO: More Generic Methods possible with LINQ
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
    {
        protected string ConnectionString { get; }
        public abstract string TableName { get; }

        protected RepositoryBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public abstract M GetSingle<P>(P pkValue);

        public abstract void Add(M entity);

        public abstract void Delete(M entity);

        public abstract void Update(M entity);

        public abstract List<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues);

        public abstract List<M> GetAll();

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
    }
}
