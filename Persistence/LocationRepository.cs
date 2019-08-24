using System;
using System.Collections.Generic;
using System.Windows;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class LocationRepository : RepositoryBase<location>
    {
        public LocationRepository() : base()
        {
        }

        public override void Delete(location entity)
        {
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    var toDelete = ctx.locations.Find(entity.location_id);
                    ctx.locations.Remove(toDelete);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
            ctx.Dispose();
        }

        public override void Update(location entity)
        {
            var ctx = new InventarisierungsloesungDB();
            {
                try
                {
                    var toUpdate = ctx.locations.Find(entity.location_id);
                    ctx.locations.Attach(toUpdate);
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
            }
            ctx.Dispose();
        }

        public List<location> GetLocationsWithCte()
        {
            var ctx = new InventarisierungsloesungDB();
            var locationsList = new List<location>();
            {
                try
                {
                    var result = ctx.cte_locations();

                    foreach (location loc in result)
                    {
                        locationsList.Add(loc);
                    }


                }

                catch (Exception e)
                {
                    MessageBox.Show("Es konnte keine Verbindung zur Datenbank hergestellt werden: " + e.Message);
                }
                ctx.Dispose();
                return locationsList;
            }
        }
    }
}
