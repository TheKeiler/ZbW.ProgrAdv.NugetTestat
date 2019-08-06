using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ZbW.ProgrAdv.NugetTestat.Model;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.ViewModel
{
    class CustomerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IQueryable<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }
        public Customer NewCustomer { get; set; }
        public string ConnectionString { get; set; }
        private ICommand _laden;
        private ICommand _InsertCustomer;

        public CustomerViewModel()
        {
            Customers = Enumerable.Empty<Customer>().AsQueryable();
            NewCustomer = new Customer();
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = ...";
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
                var customerRepository = new CustomerRepository(this.ConnectionString);
                this.NewCustomer.SetAccountNummber();
                customerRepository.Add(this.NewCustomer);
                this.Customers = customerRepository.GetAll();
                OnPropertyChanged("Customers");
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
