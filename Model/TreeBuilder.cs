using System.Collections.Generic;
using System.Linq;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    public class TreeBuilder
    {
        public LocationNode BuildTree(List<Location> locations)
        {
            if (locations == null) return new LocationNode();
            var nodeList = locations.ToList();
            var tree = FindTreeRoot(nodeList);
            BuildTree(tree, nodeList);
            return tree;
        }

        private void BuildTree(LocationNode locationNode, List<Location> descendants)
        {
            var children = descendants.Where(node => node.ParentId == locationNode.ValueObject.Id).ToArray();
            foreach (var child in children)
            {
                var branch = Map(child, locationNode);
                locationNode.AddChildNode(branch);
                descendants.Remove(child);
            }
            foreach (var branch in locationNode.ChildNodesList)
            {
                BuildTree(branch, descendants);
            }
        }

        private LocationNode FindTreeRoot(List<Location> nodes)
        {
            var rootNodes = nodes.Where(node => node.ParentId == null);
            if (rootNodes.Count() != 1) return new LocationNode();
            var rootNode = rootNodes.Single();
            nodes.Remove(rootNode);
            return Map(rootNode, null);
        }

        private LocationNode Map(Location loc, LocationNode parentnode)
        {
            return new LocationNode
            {
                ValueObject = loc,
                ParentNode = parentnode
            };
        }
    }
}
