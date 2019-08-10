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

        public Country(string name, string phoneRegex)
        {
            this.Name = name;
            this.PhoneNumberRegex = phoneRegex;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
