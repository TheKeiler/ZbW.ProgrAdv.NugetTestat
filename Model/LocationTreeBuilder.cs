using System.Collections.Generic;
using System.Linq;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    //TODO: Implement generic Baseclass
    public class LocationTreeBuilder
    {
        public Node<Location> BuildTree(IQueryable<Location> locations)
        {
            if (locations == null) return new Node<Location>();
            var nodeList = locations.ToList();
            var tree = FindTreeRoot(nodeList);
            BuildTree(tree, nodeList);
            return tree;
        }

        private void BuildTree(Node<Location> locationNode, List<Location> descendants)
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

        private Node<Location> FindTreeRoot(List<Location> nodes)
        {
            var rootNodes = nodes.Where(node => node.ParentId == null);
            if (rootNodes.Count() != 1) return new Node<Location>();
            var rootNode = rootNodes.Single();
            nodes.Remove(rootNode);
            return Map(rootNode, null);
        }

        private Node<Location> Map(Location loc, Node<Location> parentnode)
        {
            return new Node<Location>
            {
                ValueObject = loc,
                ParentNode = parentnode
            };
        }
    }
}
