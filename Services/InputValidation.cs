using System.Text.RegularExpressions;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.Services
{
    public class InputValidation
    {
        private customer Customer;
        public InputValidation(customer customer)
        {
            this.Customer = customer;
        }

        public bool HasValidCustomernumber()
        {
            var isValidNumber = false;
            if (Customer.customernumber != null)
            {
                var regex = new Regex(@"^CU[0-9]{5}$");
                isValidNumber = regex.IsMatch(Customer.customernumber);
            }

            return isValidNumber;
        }

        public bool HasValidPhonenumber()
        {
            var isValidPhonenumber = false;
            if (Customer.tel != null)
            {
                var regex = new Regex(Customer.CustomerCountry.PhoneNumberRegex);
                string trimedPhoneNumber = Regex.Replace(Customer.tel, @"\s", "");
                isValidPhonenumber = regex.IsMatch(trimedPhoneNumber);
            }
            return isValidPhonenumber;
        }

        //RFC 2822 Matches a normal email address. Does not check the top-level domain.
        public bool HasValidMailadress()
        {
            var isValidMailadress = false;
            if (Customer.eMail != null)
            {
                isValidMailadress = Regex.IsMatch(Customer.eMail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }
            return isValidMailadress;
        }

        public bool HasValidWebsite()
        {
            var isValidWebsite = false;
            if (Customer.url != null)
            {
                var regex = new Regex(@"^((http){1}s?(:\/\/){1})?(www\.)?[a-z]+\.([a-z]+\.)?[a-z]{1,3}$");
                isValidWebsite = regex.IsMatch(Customer.url);
            }
            return isValidWebsite;
        }

        // Password has to be 8-15 Characters long and must contain at least one uppercase letter and one number
        public bool HasValidPassword()
        {
            var isValidPassword = false;
            if (Customer.password != null)
            {
                var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
                isValidPassword = regex.IsMatch(Customer.password);
            }
            return isValidPassword;
        }
    }
}
