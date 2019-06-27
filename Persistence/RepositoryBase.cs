using LinqToDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : ModelBase, new()
    {
        protected string ConnectionString { get; }
        protected string RepositoryName = "inventarisierungsloesung";
        protected string ProviderName = "MySql";

        protected RepositoryBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public T GetSingle<P>(P pkValue)
        {
            var foundRowToEntityKey = new T();
            using (var ctx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    foundRowToEntityKey = (from e in ctx.GetTable<T>() where e.Id.Equals(pkValue) select e).FirstOrDefault();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
            return foundRowToEntityKey;
        }

        public void Add(T entity)
        {
            using (var ctx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    ctx.Insert<T>(entity);
                    ctx.BeginTransaction();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
        }

        public void Delete(T entity)
        {
            using (var ctx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    var foundRowToEntityKey = (from e in ctx.GetTable<T>() where e.Id.Equals(entity.Id) select e).FirstOrDefault();
                    if (foundRowToEntityKey != null)
                    {
                        ctx.Delete<T>(foundRowToEntityKey);
                    }
                    ctx.BeginTransaction();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
        }

        public void Update(T entity)
        {
            using (var ctx = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    var foundRowToEntityKey = (from e in ctx.GetTable<T>() where e.Id.Equals(entity.Id) select e).FirstOrDefault();
                    foundRowToEntityKey = entity;
                    ctx.BeginTransaction();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> whereClause)
        {
            IQueryable<T> entities = Enumerable.Empty<T>().AsQueryable();
            using (var db = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    entities = db.GetTable<T>().Where<T>(whereClause);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
            return entities;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> entities = Enumerable.Empty<T>().AsQueryable();
            using (var db = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    entities = db.GetTable<T>();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
            return entities;
        }

        public long Count(Expression<Func<T, bool>> whereClause)
        {
            IQueryable<T> entities = Enumerable.Empty<T>().AsQueryable();
            using (var db = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    entities = db.GetTable<T>().Where<T>(whereClause);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
            return entities.Count();
        }
        public long Count()
        {
            IQueryable<T> entities = Enumerable.Empty<T>().AsQueryable();
            using (var db = new LinqToDB.DataContext(ProviderName, ConnectionString))
            {
                try
                {
                    entities = db.GetTable<T>();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
            return entities.Count();
        }
    }
}
