using System.Collections.Generic;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    public class DuplicateChecker
    {
        public IEnumerable<IEntity> FindDuplicates(IEnumerable<IEntity> list)
        {
            var hashSet = new HashSet<IEntity>();
            var ret = new List<IEntity>();
            foreach (var item in list)
            {
                if (!hashSet.Add(item))
                {
                    ret.Add(item);
                }
            }

            return ret;
        }
    }

}
