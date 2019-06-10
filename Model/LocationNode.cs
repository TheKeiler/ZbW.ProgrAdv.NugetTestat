using System.Collections.Generic;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    public class LocationNode
    {
        public Location ValueObject { get; set; }
        public LocationNode ParentNode { get; set; }
        public List<LocationNode> ChildNodesList { get; set; }

        public LocationNode()
        {
            this.ChildNodesList = new List<LocationNode>();
        }
        public LocationNode(Location valueObject)
        {
            this.ValueObject = valueObject;
            this.ParentNode = null;
            this.ChildNodesList = new List<LocationNode>();
        }

        public LocationNode(LocationNode parentNode, Location valueObject)
        {
            this.ValueObject = valueObject;
            this.ParentNode = parentNode;
            this.ChildNodesList = new List<LocationNode>();
        }

        public void AddChildNode(LocationNode childNode)
        {
            this.ChildNodesList.Add(childNode);
        }
    }

}

