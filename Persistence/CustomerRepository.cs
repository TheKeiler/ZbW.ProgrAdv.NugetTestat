using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
