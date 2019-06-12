using System.Collections.Generic;

namespace ZbW.ProgrAdv.NugetTestat.Model
{
    public class Node<M>
    {
        public M ValueObject { get; set; }
        public Node<M> ParentNode { get; set; }
        public List<Node<M>> ChildNodesList { get; set; }

        public Node()
        {
            this.ChildNodesList = new List<Node<M>>();
        }
        public Node(M valueObject)
        {
            this.ValueObject = valueObject;
            this.ParentNode = default;
            this.ChildNodesList = new List<Node<M>>();
        }

        public Node(Node<M> parentNode, M valueObject)
        {
            this.ValueObject = valueObject;
            this.ParentNode = parentNode;
            this.ChildNodesList = new List<Node<M>>();
        }

        public void AddChildNode(Node<M> childNode)
        {
            this.ChildNodesList.Add(childNode);
        }

        public override string ToString()
        {
            return ValueObject.ToString();
        }
    }
}
