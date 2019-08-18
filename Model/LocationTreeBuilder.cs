using System.Collections.Generic;
using System.Linq;
using ZbW.ProgrAdv.NugetTestat.Persistence;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    //TODO: Implement generic Baseclass
    public class LocationTreeBuilder
    {

        public Node<location> BuildTree(List<location> locations)
        {
            if (locations == null) return new Node<location>();
            var nodeList = locations.ToList();
            var tree = FindTreeRoot(nodeList);
            nodeList.Remove(tree.ValueObject);
            BuildTree(tree, nodeList);
            return tree;
        }

        private void BuildTree(Node<location> locationNode, List<location> descendants)
        {
            var children = descendants.Where(node => node.parent_location == locationNode.ValueObject.location_id).ToArray();
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

        private Node<location> FindTreeRoot(List<location> nodes)
        {
            var rootNodes = nodes.Where(node => node.parent_location == 0);
            if (rootNodes.Count() != 1) return new Node<location>();
            var rootNode = rootNodes.Single();
            //nodes.Remove(rootNode);
            return Map(rootNode, null);
        }

        private Node<location> Map(location loc, Node<location> parentnode)
        {
            return new Node<location>
            {
                ValueObject = loc,
                ParentNode = parentnode
            };
        }
    }
}
