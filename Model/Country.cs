using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    public class Country
    {
        public string Name { get; set; }
        public string PhoneNumberRegex { get; set; }

        public Country(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
