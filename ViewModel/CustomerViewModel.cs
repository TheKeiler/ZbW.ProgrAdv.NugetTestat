using System;
using System.Collections.Generic;
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
        public List<customer> Customers { get; set; }
        public customer SelectedCustomer { get; set; }
        public customer NewCustomer { get; set; }
        public ObservableCollection<Country> Countries { get; set; }
        public Country SelectedCountry { get; set; }
        public string ConnectionString { get; set; }
        private ICommand _laden;
        private ICommand _InsertCustomer;
        private ICommand _DeleteCustomer;
        private ICommand _AlterCustomer;
        private ICommand _SaveChangedCustomer;

        public CustomerViewModel()
        {
            Customers = Enumerable.Empty<customer>().AsQueryable().ToList();
            NewCustomer = new customer();
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
            var customerRepo = new CustomerRepository();
            this.Customers = customerRepo.GetAll().ToList();
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
                var customerRepository = new CustomerRepository();
                customerRepository.Add(this.NewCustomer);
                this.Customers = customerRepository.GetAll().ToList();
                OnPropertyChanged("Customers");
            }
        }

        public void DeleteCustomerData()
        {
            var customerRepository = new CustomerRepository();
            customerRepository.Delete(this.SelectedCustomer);
            this.Customers = customerRepository.GetAll().ToList();
            ChangeSelectedCustomer();
            OnPropertyChanged("SelectedCustomer");
            OnPropertyChanged("Customers");
        }

        public void SaveChangedCustomerData()
        {
            var customerRepository = new CustomerRepository();
            if (AreCustomerInputsValid(this.SelectedCustomer))
            {
                customerRepository.Update(this.SelectedCustomer);
                OnPropertyChanged("SelectedCustomer");
                OnPropertyChanged("Customers");
            }
        }

        public void AlterCustomerData()
        {
            WindowAlterCustomer windowAlterCustomer = new WindowAlterCustomer();
            windowAlterCustomer.DataContext = this;
            windowAlterCustomer.Show();
        }

        private void HashCustomerPassword()
        {
            var cryptoService = new CryptoService();
            var hash = cryptoService.ComputeHash(NewCustomer.password, cryptoService.GenerateSalt(), 10101, 24);
            NewCustomer.password = Convert.ToBase64String(hash);
        }

        private void ChangeSelectedCustomer()
        {
            if (this.Customers.Any())
            {
                this.SelectedCustomer = Customers.First();
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public bool AreCustomerInputsValid(customer customer)
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

        public ICommand DeleteCustomer
        {
            get
            {
                if (_DeleteCustomer == null)
                {
                    _DeleteCustomer = new RelayCommand(
                        param => DeleteCustomerData()
                    );
                }
                return _DeleteCustomer;
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


    }
}
