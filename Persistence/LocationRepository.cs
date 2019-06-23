using System;
using System.Collections.Generic;
using System.Data;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    //TODO: Refactor Class with LINQ
    public class LocationRepository : RepositoryBase<Location>
    {
        public override string TableName => "location";
        public override string TablePrimaryKey => "location_id";

        public LocationRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
