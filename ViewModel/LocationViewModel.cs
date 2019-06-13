using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ZbW.ProgrAdv.NugetTestat.Model;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.ViewModel
{
    public class LocationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Location> Locations { get; set; }
        public string ConnectionString { get; set; }
        public List<Node<Location>> LocationTree { get; set; }
        private ICommand _laden;

        public LocationViewModel()
        {
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = ...";
        }

        public void GetAllLocations()
        {
            var locationRepo = new LocationRepository(ConnectionString);
            this.Locations = locationRepo.GetAll();
            this.LocationTree = new List<Node<Location>>();
            GenerateLocationTreeFromList(Locations);
            PropertyChanged(this, new PropertyChangedEventArgs("LocationTree"));
        }

        public void GenerateLocationTreeFromList(List<Location> locationList)
        {
            var treeBuilder = new LocationTreeBuilder();
            var locationNode = treeBuilder.BuildTree(locationList);
            this.LocationTree.Add(locationNode);
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
                        param => GetAllLocations()
                    );
                }
                return _laden;
            }
        }
    }
}
