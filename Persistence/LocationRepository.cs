using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class LocationRepository : RepositoryBase<Location>
    {
        public LocationRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
