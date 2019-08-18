using System;
using System.Windows;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class CustomerRepository : RepositoryBase<customer>
    {
        public CustomerRepository() : base()
        {
        }

        public override void Delete(customer entity)
        {
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    var toDelete = ctx.customers.Find(entity.customer_id);
                    ctx.customers.Remove(toDelete);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
        }

        public override void Update(customer entity)
        {
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    var toUpdate = ctx.customers.Find(entity.customer_id);
                    ctx.customers.Attach(toUpdate);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
        }
    }
}
