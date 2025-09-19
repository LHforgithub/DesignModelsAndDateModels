using MDM.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Examples
{
    internal class GraphNodeExample : IGraphNode<GraphNodeExample, GraphEdgeExample>,
        IGraphNodePath<GraphNodeExample, GraphEdgeExample>
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
        public GraphEdgeExample FromEdge { get; set; }
        public GraphNodeExample Current => this;
        public uint G { get; set; }
        public uint Heuristic(GraphNodeExample otherNode) => 0;
        public GraphEdgeExample AddNewEdgeTo(GraphNodeExample otherNode, uint weight)
        {
            if (otherNode == null || otherNode == this)
                return default;
            var edge = new GraphEdgeExample(this, otherNode, weight);
            OutEdges.Add(edge);
            otherNode.InEdges.Add(edge);
            return edge;
        }
        public bool TryRemoveAllEdgeTo(GraphNodeExample otherNode, out List<GraphEdgeExample> edges)
        {
            edges = new();
            if (otherNode == null || otherNode == this)
                return false;
            edges = OutEdges.AllTo(otherNode).ToList();
            if (edges.Count > 0)
            {
                foreach (var edge in edges)
                {
                    OutEdges.Remove(edge);
                    otherNode.InEdges.Remove(edge);
                }
                return true;
            }
            return false;
        }
        public bool TryRemoveAllEdgeFrom(GraphNodeExample otherNode, out List<GraphEdgeExample> edges)
        {
            edges = new();
            if (otherNode == null || otherNode == this)
                return false;
            edges = InEdges.AllFrom(otherNode).ToList();
            if (edges.Count > 0)
            {
                foreach (var edge in edges)
                {
                    InEdges.Remove(edge);
                    otherNode.OutEdges.Remove(edge);
                }
                return true;
            }
            return false;
        }

        public bool AddNewEdge(GraphEdgeExample edge)
        {
            if (edge.IsTo(this))
            {
                InEdges.Add(edge);
                edge.FromNode.OutEdges.Add(edge);
                return true;
            }
            else if (edge.IsFrom(this))
            {
                OutEdges.Add(edge);
                edge.ToNode.InEdges.Add(edge);
                return true;
            }
            else 
                return false;
        }

        public bool RemoveEdge(GraphEdgeExample edge)
        {
            if (edge.IsTo(this))
            {
                InEdges.Remove(edge);
                edge.FromNode.OutEdges.Remove(edge);
                return true;
            }
            else if (edge.IsFrom(this))
            {
                OutEdges.Remove(edge);
                edge.ToNode.InEdges.Remove(edge);
                return true;
            }
            else
                return false;
        }
        public void RemoveAllEdges()
        {
            foreach (var edge in InEdges.ToList())
            {
                edge.FromNode.OutEdges.Remove(edge);
            }
            InEdges.Clear();
            foreach (var edge in OutEdges.ToList())
            {
                edge.ToNode.InEdges.Remove(edge);
            }
            OutEdges.Clear();
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

        public override string ToString() => $"<{FromNode} -> {ToNode}>({LinkWeight})";
    }
}
