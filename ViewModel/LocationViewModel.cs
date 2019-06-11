using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZbW.ProgrAdv.NugetTestat.Model;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.ViewModel
{
    public class LocationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<Location> Locations { get; set; }
        public string ConnectionString { get; set; }
        public Node<Location> LocationTree { get; set; }

        public LocationViewModel()
        {
            this.ConnectionString = "Server = localhost; Database = inventarisierungsloesung; Uid = root; Pwd = eli";
            this.Locations = GetAllLocations();
            GenerateLocationTreeFromList(this.Locations);
        }

        public List<Location> GetAllLocations()
        {
            var locationRepo = new LocationRepository(ConnectionString);
            return locationRepo.GetAll();
        }

        public Node<Location> GenerateLocationTreeFromList(List<Location> locationList)
        {
            var treeBuilder = new LocationTreeBuilder();
            var locationNode = treeBuilder.BuildTree(locationList);
            return locationNode;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
