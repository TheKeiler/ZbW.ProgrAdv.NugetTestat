using System.Collections.Generic;

namespace ZbW.ProgrAdv.NugetTestat.Persistence
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M>
    {
        public M GetSingle<P>(P pkValue)
        {
            throw new System.NotImplementedException();
        }

        public void Add(M entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(M entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(M entity)
        {
            throw new System.NotImplementedException();
        }

        public List<M> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new System.NotImplementedException();
        }

        public List<M> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public long Count(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new System.NotImplementedException();
        }

        public long Count()
        {
            throw new System.NotImplementedException();
        }

        public string TableName { get; }
    }
}
