using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ZbW.ProgrAdv.NugetTestat.Control;
using ZbW.ProgrAdv.NugetTestat.Model;
using ZbW.ProgrAdv.NugetTestat.Persistence;
using ZbW.ProgrAdv.NugetTestat.Services;

namespace ZbW.ProgrAdv.NugetTestat.ViewModel
{
    class CustomerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IQueryable<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }
        public Customer NewCustomer { get; set; }
        public ObservableCollection<Country> Countries { get; set; }
        public Country SelectedCountry { get; set; }
        public string ConnectionString { get; set; }
        private ICommand _laden;
        private ICommand _InsertCustomer;
        private ICommand _AlterCustomer;
        private ICommand _SaveChangedCustomer;

        public CustomerViewModel()
        {
            Customers = Enumerable.Empty<Customer>().AsQueryable();
            NewCustomer = new Customer();
            Countries = new ObservableCollection<Country>();
            GenerateListOfCountries();
            SelectedCountry = Countries[0];
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = ...";
        }

        private void GenerateListOfCountries()
        {
            Countries.Add(new Country("Schweiz", @"^(0041|0|\+41)(\(0\))?([0-9]{2}\/?[0-9]{7})$"));
            Countries.Add(new Country("Deutschland", @"^(0049|0|\+49)(\(0\))?([0-9]{2}\/?[0-9]{8})$"));
            Countries.Add(new Country("Liechtenstein", @"^(00423|\+423)(\/?[0-9]{3}\/?[0-9]{4})$"));
        }

        public void GetAllCustomers()
        {
            var customerRepo = new CustomerRepository(ConnectionString);
            this.Customers = customerRepo.GetAll();
            ChangeSelectedCustomer();
            OnPropertyChanged("Customers");
        }

        public void InsertNewCustomer()
        {
            this.NewCustomer.SetAccountNummber();
            this.NewCustomer.CustomerCountry = SelectedCountry;

            if (AreCustomerInputsValid(this.NewCustomer) == true)
            {
                HashCustomerPassword();
                var customerRepository = new CustomerRepository(this.ConnectionString);
                customerRepository.Add(this.NewCustomer);
                this.Customers = customerRepository.GetAll();
                OnPropertyChanged("Customers");
            }
        }

        public void AlterCustomerData()
        {
            var windowAlterCustomer = new WindowAlterCustomer();
            OnPropertyChanged("SelectedCustomer");
            OnPropertyChanged("Customers");
            windowAlterCustomer.Show();

        }

        public void SaveChangedCustomerData()
        {
            var customerRepository = new CustomerRepository(this.ConnectionString);
            customerRepository.Update(this.SelectedCustomer);
            ChangeSelectedCustomer();
            OnPropertyChanged("SelectedCustomer");
            OnPropertyChanged("Customers");
        }

        private void HashCustomerPassword()
        {
            var cryptoService = new CryptoService();
            var hash = cryptoService.ComputeHash(NewCustomer.Password, cryptoService.GenerateSalt(), 10101, 24);
            NewCustomer.Password = Convert.ToBase64String(hash);
        }

        private void ChangeSelectedCustomer()
        {
            if (this.Customers.Any())
            {
                this.SelectedCustomer = Customers.First();
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public bool AreCustomerInputsValid(Customer customer)
        {
            var validator = new InputValidation(customer);

            if (validator.HasValidCustomernumber() == false)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Kundennummer ein. Beispiel: CU12345", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (validator.HasValidPhonenumber() == false)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Telefonnummer ein.", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (validator.HasValidMailadress() == false)
            {
                MessageBox.Show("Bitte geben Sie eine gültige EMail-Adresse ein.", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (validator.HasValidWebsite() == false)
            {
                MessageBox.Show("Bitte geben Sie eine gültige Website-Url ein.", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (validator.HasValidPassword() == false)
            {
                MessageBox.Show("Bitte geben Sie ein gültiges Passwort ein. Das Passwort muss 8-15 Zeichen und minimal ein Grossbuchstabe und ein Sonderzeichen enthalten.", "Validator", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand Laden
        {
            get
            {
                if (_laden == null)
                {
                    _laden = new RelayCommand(
                        param => GetAllCustomers()
                    );
                }
                return _laden;
            }
        }

        public ICommand InsertCustomer
        {
            get
            {
                if (_InsertCustomer == null)
                {
                    _InsertCustomer = new RelayCommand(
                        param => InsertNewCustomer()
                    );
                }
                return _InsertCustomer;
            }
        }

        public ICommand AlterCustomer
        {
            get
            {
                if (_AlterCustomer == null)
                {
                    _AlterCustomer = new RelayCommand(
                        param => AlterCustomerData()
                    );
                }
                return _AlterCustomer;
            }
        }

        public ICommand SaveChangedCustomer
        {
            get
            {
                if (_SaveChangedCustomer == null)
                {
                    _SaveChangedCustomer = new RelayCommand(
                        param => SaveChangedCustomerData()
                    );
                }
                return _SaveChangedCustomer;
            }
        }


    }
}
