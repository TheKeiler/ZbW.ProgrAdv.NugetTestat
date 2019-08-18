using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ZbW.ProgrAdv.NugetTestat.Model;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.ViewModel
{
    public class LocationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<location> Locations { get; set; }
        public List<Node<location>> LocationTree { get; set; }
        private ICommand _laden;

        public LocationViewModel()
        {
            Locations = new List<location>();
        }

        public void GetAllLocations()
        {
            var locationRepo = new LocationRepository();
            this.Locations = locationRepo.GetAll().ToList();
            this.LocationTree = new List<Node<location>>();
            GenerateLocationTreeFromList(Locations);
            OnPropertyChanged("LocationTree");
        }

        public void GenerateLocationTreeFromList(List<location> locationList)
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
