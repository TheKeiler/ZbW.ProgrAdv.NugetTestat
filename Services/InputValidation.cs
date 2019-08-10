using System.Text.RegularExpressions;
using System.Windows;
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
            var isValidNumber = false;
            if (Customer.CustomerNumber != null)
            {
                var regex = new Regex(@"^CU[0-9]{5}$");
                isValidNumber = regex.IsMatch(Customer.CustomerNumber);
            }

            if (isValidNumber == false)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Kundennummer ein. Beispiel: CU12345", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return isValidNumber;
        }

        public bool HasValidPhonenumber()
        {
            var isValidPhonenumber = false;
            if (Customer.PhoneNumber != null)
            {
                var regex = new Regex(Customer.CustomerCountry.PhoneNumberRegex);
                string trimedPhoneNumber = Regex.Replace(Customer.PhoneNumber, @"s", "");
                isValidPhonenumber = regex.IsMatch(trimedPhoneNumber);
            }

            if (isValidPhonenumber == false)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Telefonnummer ein.", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return isValidPhonenumber;
        }

        //RFC 2822 Matches a normal email address. Does not check the top-level domain.
        public bool HasValidMailadress()
        {
            var isValidMailadress = false;
            if (Customer.EMail != null)
            {
                isValidMailadress = Regex.IsMatch(Customer.EMail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            }

            if (isValidMailadress == false)
            {
                MessageBox.Show("Bitte geben Sie eine gültige EMail-Adresse ein.", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return isValidMailadress;
        }

        public bool HasValidWebsite()
        {
            var isValidWebsite = false;
            if (Customer.Url != null)
            {
                var regex = new Regex(@"^((http){1}s?(:\/\/){1})?(www\.)?[a-z]+\.{1}([a-z]+\.{1})?[a-z]+");
                isValidWebsite = regex.IsMatch(Customer.Url);
            }

            if (isValidWebsite == false)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Website-Url ein.", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return isValidWebsite;
        }

        // Password has to be 8-15 Characters long and must contain at least one uppercase letter and one number
        public bool HasValidPassword()
        {
            var isValidPassword = false;
            if (Customer.Password != null)
            {
                var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
                isValidPassword = regex.IsMatch(Customer.Password);
            }

            if (isValidPassword == false)
            {
                MessageBox.Show("Bitte geben Sie ein gültiges Passwort ein. Das Passwort muss 8-15 Zeichen und minimal ein Grossbuchstabe und ein Sonderzeichen enthalten.", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return isValidPassword;
        }

        public bool AreCustomerInputsValid()
        {
            var array = new bool[5]{
            HasValidCustomernumber(),
            HasValidPhonenumber(),
            HasValidMailadress(),
            HasValidWebsite(),
            HasValidPassword()
            };

            var everyElementIsTrue = true;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] == false)
                {
                    everyElementIsTrue = false;
                }
            }

            return everyElementIsTrue;
        }
    }
}
