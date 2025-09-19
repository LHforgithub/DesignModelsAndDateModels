using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
namespace MDM.Graph
{
    public interface IGraphNode<T, U> where T : IGraphNode<T, U> where U : IGraphEdge<T, U>
    {
        HashSet<U> InEdges { get; }
        HashSet<U> OutEdges { get; }
        bool TryAddEdgeTo(T otherNode, uint weight, out U edge);
        bool TryRemoveEdgeTo(T otherNode, out U edge);
        IGraphNodePath<T, U> PathValue { get; }
    }
    public interface IGraphEdge<out T, out U> where T : IGraphNode<T, U> where U : IGraphEdge<T, U>
    {
        T FromNode { get; }
        T ToNode { get; }
        uint LinkWeight { get; }
    }
    public interface IGraphNodePath<T, U> where T : IGraphNode<T, U> where U : IGraphEdge<T, U>
    {
        IGraphNodePath<T, U> Father { get; set; }
        T Current { get; }
        uint Heuristic(T otherNode);
        uint G { get; set; }
        void ResetPathFound();
    }

    //public class Graph<TNode, TEdge> where TNode : IGraphNode where TEdge : IGraphEdge
    //{
    //    internal HashSet<TNode> Nodes { get; } = new HashSet<TNode>();
    //    internal HashSet<TEdge> Edges { get; } = new HashSet<TEdge>();
    //}
}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
