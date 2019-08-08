using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZbW.ProgrAdv.NugetTestat.Model;

namespace ZbW.ProgrAdv.NugetTestat.Services
{
    public class InputValidation
    {
        private Customer Customer;
        public InputValidation(Customer customer)
        {
            this.Customer = customer;
        }

        public bool HasValidCustomernumber()
        {
            var regex = new Regex("/^CU{1}[0-9]{5}/");
            return regex.IsMatch(Customer.CustomerNumber);
        }

        public bool HasValidPhonenumber()
        {
            var regex = new Regex(Customer.CustomerCountry.PhoneNumberRegex);
            return regex.IsMatch(Customer.PhoneNumber);
        }

        //RFC 2822 Matches a normal email address. Does not check the top-level domain.
        public bool HasValidMailadress()
        {
            return Regex.IsMatch(Customer.EMail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public bool HasValidWebsite()
        {
            var regex = new Regex(@"^((http){1}s?(:\/\/){1})?(www\.)?[a-z]+\.{1}([a-z]+\.{1})?[a-z]+");
            return regex.IsMatch(Customer.Url);
        }

        // Password has to be 8-15 Characters long and must contain at least one uppercase letter and one number
        public bool HasValidPassword()
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
            return regex.IsMatch(Customer.Password);
        }
    }
}
