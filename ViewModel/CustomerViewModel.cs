using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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

        //TODO: Validierung mit REGEX
        public void InsertNewCustomer()
        {
            this.NewCustomer.SetAccountNummber();
            this.NewCustomer.CustomerCountry = SelectedCountry;
            var validator = new InputValidation(this.NewCustomer);

            if(validator.AreCustomerInputsValid() == true)
            {
                var customerRepository = new CustomerRepository(this.ConnectionString);
                customerRepository.Add(this.NewCustomer);
                this.Customers = customerRepository.GetAll();
                OnPropertyChanged("Customers");
            }               
        }

        private void ChangeSelectedCustomer()
        {
            if (this.Customers.Any())
            {
                this.SelectedCustomer = Customers.First();
            }
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
    }
}
