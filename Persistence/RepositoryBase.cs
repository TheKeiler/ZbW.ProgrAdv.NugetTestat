using LinqToDB;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : ModelBase, new()
    {
        protected string RepositoryName = "inventarisierungsloesung";
        protected string ProviderName = "System.Data.SqlClient";

        public T GetSingle<P>(P pkValue)
        {
            var foundRowToEntityKey = new T();
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    foundRowToEntityKey = (from e in ctx.Set<T>() where e.Equals(pkValue) select e).FirstOrDefault();
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
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    ctx.Set<T>().Add(entity);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
        }

        public abstract void Delete(T entity);

        public abstract void Update(T entity);

        public IQueryable<T> GetAll(Expression<Func<T, bool>> whereClause)
        {
            IQueryable<T> entities = Enumerable.Empty<T>().AsQueryable();
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    entities = ctx.Set<T>().Where<T>(whereClause);
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

            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    entities = ctx.Set<T>();
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
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    entities = ctx.Set<T>().Where<T>(whereClause);
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
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    entities = ctx.Set<T>();
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
