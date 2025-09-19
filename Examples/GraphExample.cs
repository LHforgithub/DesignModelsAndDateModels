using MDM.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
namespace MDM.Examples
{
    internal class GraphNodeExample : IGraphNode<GraphNodeExample, GraphEdgeExample>, IGraphNodePath<GraphNodeExample, GraphEdgeExample>
    {
        public GraphNodeExample(string name)
        {
            Name = name;
        }
        public string Name { get; }
        public override string ToString() => Name;
        public HashSet<GraphEdgeExample> InEdges { get; } = new();
        public HashSet<GraphEdgeExample> OutEdges { get; } = new();
        public IGraphNodePath<GraphNodeExample, GraphEdgeExample> PathValue => this;
        public IGraphNodePath<GraphNodeExample, GraphEdgeExample> Father { get; set; }
        public GraphNodeExample Current => this;
        public uint G { get; set; } = 0;
        public uint Heuristic(GraphNodeExample otherNode)
        {
            return 0;
        }
        public void ResetPathFound()
        {
            G = 0;
            Father = null;
        }
        public bool TryAddEdgeTo(GraphNodeExample otherNode, uint weight, out GraphEdgeExample edge)
        {
            edge = null;
            if (otherNode == null)
                return false;
            edge = OutEdges.IsTo(otherNode);
            if (edge == null)
            {
                edge = new GraphEdgeExample(this, otherNode, weight);
                OutEdges.Add(edge);
                otherNode.InEdges.Add(edge);
                return true;
            }
            return true;
        }
        public bool TryRemoveEdgeTo(GraphNodeExample otherNode, out GraphEdgeExample edge)
        {
            edge = null;
            if (otherNode == null)
                return false;
            edge = OutEdges.IsTo(otherNode);
            if (edge == null)
                return false;
            return true;
        }
    }
    internal class GraphEdgeExample : IGraphEdge<GraphNodeExample, GraphEdgeExample>
    {
        public GraphEdgeExample(GraphNodeExample from, GraphNodeExample to, uint weight = 0)
        {
            FromNode = from;
            ToNode = to;
            LinkWeight = weight;
        }
        public GraphNodeExample FromNode { get; }
        public GraphNodeExample ToNode { get; }
        public uint LinkWeight { get; set; }
    }
}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释