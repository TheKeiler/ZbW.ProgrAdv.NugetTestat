using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        public LocationViewModel()
        {
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = eli";
            this.Locations = GetAllLocations();
            this.LocationTree = new List<Node<Location>>();
            GenerateLocationTreeFromList(this.Locations);
        }

        public List<Location> GetAllLocations()
        {
            var locationRepo = new LocationRepository(ConnectionString);
            return locationRepo.GetAll();
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
    }
}
