using MDM.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MDM.Graph
{
    /// <summary>
    /// 图数据拓展方法
    /// </summary>
    public static class GraphExtensions
    {
        #region Back
        //Node
        //public static IGraphEdge GetEdge(this IGraphNode node, IGraphNode other)
        //{
        //    return node.Edges.FirstOrDefault(e => e.GetConnected(node) == other);
        //}
        //public static int GetWeight(this IGraphNode node, IGraphNode other)
        //{
        //    return node.GetEdge(other).GetWeight(node);
        //}
        //public static bool IsConnected(this IGraphNode node, IGraphNode other)
        //{
        //    return node.GetWeight(other) != 0;
        //}
        //public static bool IsCanPassToOpposite(this IGraphNode node, IGraphNode other)
        //{
        //    return node.GetWeight(other) > 0;
        //}
        ////Edge
        //public static bool IsValid(this IGraphEdge edge)
        //{
        //    return edge != default && edge.Left != default && edge.Right != default
        //         && edge.Left != edge.Right;
        //}
        //public static IGraphNode GetConnected(this IGraphEdge edge, IGraphNode one)
        //{
        //    if (one == null || !edge.IsValid()) return default;
        //    if (edge.Left == one) return edge.Right;
        //    else if (edge.Right == one) return edge.Left;
        //    return default;
        //}
        //public static int GetWeight(this IGraphEdge edge, IGraphNode node)
        //{
        //    return edge.GetConnected(node) is IGraphNode graphNode ?
        //        graphNode == edge.Left ? edge.L2RState :
        //        graphNode == edge.Right ? edge.R2LState : 0
        //        : 0;
        //}
        //public static bool IsCanPassToOpposite(this IGraphEdge edge, IGraphNode node)
        //{
        //    return edge.GetWeight(node) > 0;
        //}
        //public static void ConnectBoth(this IGraphEdge edge)
        //{
        //    if (!edge.IsValid()) return;
        //    if (!edge.Left.Edges.Contains(edge))
        //        edge.Left.AddEdge(edge);
        //    if (!edge.Right.Edges.Contains(edge))
        //        edge.Right.AddEdge(edge);
        //    if (!edge.Left.Neighbors.Contains(edge.Right))
        //        edge.Left.AddNeighbor(edge.Right);
        //    if (!edge.Right.Neighbors.Contains(edge.Left))
        //        edge.Right.AddNeighbor(edge.Left);
        //}
        //public static void DisconnectBoth(this IGraphEdge edge)
        //{
        //    if (!edge.IsValid()) return;
        //    if (edge.Left.Edges.Contains(edge))
        //        edge.Left.RemoveEdge(edge);
        //    if (edge.Right.Edges.Contains(edge))
        //        edge.Right.RemoveEdge(edge);
        //    if (edge.Left.Neighbors.Contains(edge.Right))
        //        edge.Left.RemoveNeighbor(edge.Right);
        //    if (edge.Right.Neighbors.Contains(edge.Left))
        //        edge.Right.RemoveNeighbor(edge.Left);
        //}
        //Graph
        //public static bool AddNode<TNode, TEdge>(this Graph<TNode, TEdge> graph, TNode node)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    return graph.Nodes.Add(node);
        //}
        //public static bool AddEdge<TNode, TEdge>(this Graph<TNode, TEdge> graph, TEdge edge)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    if (graph.Edges.Contains(edge))
        //        return false;
        //    var left = (TNode)edge.Left;
        //    var right = (TNode)edge.Right;
        //    if (left is null || right is null)
        //        throw new ArrayTypeMismatchException();
        //    graph.AddNode(left);
        //    graph.AddNode(right);
        //    edge.ConnectBoth();
        //    return graph.Edges.Add(edge);
        //}
        //public static bool AddEdge<TNode, TEdge>(this Graph<TNode, TEdge> graph, TNode left, TNode right, int L2R = 1, int R2L = 1)
        //    where TNode : IGraphNode where TEdge : IGraphEdge, new()
        //{
        //    var edge = new TEdge();
        //    edge.Left = left;
        //    edge.Right = right;
        //    if (!graph.AddEdge(edge))
        //        return false;
        //    edge.L2RState = L2R;
        //    edge.R2LState = R2L;
        //    return true;
        //}
        //public static TNode RemoveNode<TNode, TEdge>(this Graph<TNode, TEdge> graph, TNode node)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    if(!graph.Nodes.Remove(node))
        //        return default;
        //    foreach(var edge in node.Edges)
        //    {
        //        edge.DisconnectBoth();
        //        graph.Edges.Remove((TEdge)edge);
        //    }
        //    return node;
        //}
        //public static TEdge RemoveEdge<TNode, TEdge>(this Graph<TNode, TEdge> graph, TEdge edge)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    if(!graph.Edges.Remove(edge))
        //        return default;
        //    edge.DisconnectBoth();
        //    return edge;
        //}
        //public static TEdge RemoveEdge<TNode, TEdge>(this Graph<TNode, TEdge> graph, TNode left, TNode right)
        //   where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    if(!graph.Nodes.Contains(left) || !graph.Nodes.Contains(right))
        //        return default;
        //    var remove = left.Edges.FirstOrDefault(e => e.GetConnected(left).Equals(right));
        //    if (remove == null || remove is not TEdge result || !graph.Edges.Contains(result))
        //        return default;
        //    return graph.RemoveEdge(result);
        //}
        //public static bool Contains<TNode, TEdge>(this Graph<TNode, TEdge> graph, TNode node)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    return graph.Nodes.Contains(node);
        //}
        //public static bool Contains<TNode, TEdge>(this Graph<TNode, TEdge> graph, TEdge edge)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    return graph.Edges.Contains(edge);
        //}
        //public static bool Any<TNode, TEdge>(this Graph<TNode, TEdge> graph, Func<TEdge, bool> predicateEdge = null, Func<TNode, bool> predicateNode= null)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    if (predicateNode == null && predicateEdge == null) return true;
        //    var flag1 = predicateNode != null && graph.Nodes.Any(predicateNode);
        //    var flag2 = predicateEdge != null && graph.Edges.Any(predicateEdge);
        //    return flag1 && flag2;
        //}
        //public static bool All<TNode, TEdge>(this Graph<TNode, TEdge> graph, Func<TEdge, bool> predicateEdge = null, Func<TNode, bool> predicateNode = null)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    if (predicateNode == null && predicateEdge == null) return false;
        //    var flag1 = predicateNode != null && graph.Nodes.All(predicateNode);
        //    var flag2 = predicateEdge != null && graph.Edges.All(predicateEdge);
        //    return flag1 && flag2;
        //}
        //public static Graph<TNode, TEdge> ForEachNode<TNode, TEdge>(this Graph<TNode, TEdge> graph, Action<TNode> action)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    foreach (var item in new List<TNode>(graph.Nodes))
        //    {
        //        action(item);
        //    }
        //    return graph;
        //}
        //public static Graph<TNode, TEdge> ForEachEdge<TNode, TEdge>(this Graph<TNode, TEdge> graph, Action<TEdge> action)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    foreach (var item in new List<TEdge>(graph.Edges))
        //    {
        //        action(item);
        //    }
        //    return graph;
        //}
        //public static IEnumerable<TNode> FindAllNode<TNode, TEdge>(this Graph<TNode, TEdge> graph, Func<TNode, bool> preidicate)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    foreach(var item in new List<TNode>(graph.Nodes))
        //    {
        //        if(preidicate(item))
        //            yield return item;
        //    }
        //}
        //public static IEnumerable<TEdge> FindAllEdge<TNode, TEdge>(this Graph<TNode, TEdge> graph, Func<TEdge, bool> preidicate)
        //    where TNode : IGraphNode where TEdge : IGraphEdge
        //{
        //    foreach (var item in new List<TEdge>(graph.Edges))
        //    {
        //        if (preidicate(item))
        //            yield return item;
        //    }
        //}
        //public static Queue<TEdge> FindPath<TNode, TEdge>(this Graph<TNode, TEdge> graph, TNode start, TNode end, List<TEdge> checkedList = null)
        //    where TNode : class, IGraphNode where TEdge : class, IGraphEdge
        //{
        //    if (start == null || end == null) return new Queue<TEdge>();
        //    checkedList ??= new List<TEdge>();
        //    foreach (var edge in end.Edges.ToList())
        //    {
        //        if(edge is not TEdge tEdge) continue;
        //        if (checkedList.Contains(tEdge) || !graph.Edges.Contains(tEdge)) continue;
        //        checkedList.Add(tEdge);
        //        if (tEdge.GetConnected(end) is not TNode connect) continue;
        //        if (connect == start)
        //        {
        //            var result = new Queue<TEdge>();
        //            result.Enqueue(tEdge);
        //            return result;
        //        }
        //        var queue = graph.FindPath(start, connect, checkedList);
        //        if (queue.Count > 0)
        //        {
        //            queue.Enqueue(tEdge);
        //            return queue;
        //        }
        //    }
        //    return new Queue<TEdge>();
        //}

        #endregion

        /// <summary>
        /// 判断边是否指向指定节点
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="edge">要检查的边</param>
        /// <param name="toNode">目标节点</param>
        /// <returns>如果边指向目标节点则返回true</returns>
        public static bool IsTo<TNode, TEdge>(this TEdge edge, TNode toNode)
            where TNode : IGraphNode<TNode, TEdge>
            where TEdge : IGraphEdge<TNode, TEdge>
        {
            return edge.ToNode.Equals(toNode);
        }
        /// <summary>
        /// 判断边是否从指定节点发出
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="edge">要检查的边</param>
        /// <param name="fromNode">源节点</param>
        /// <returns>如果边从源节点发出则返回true</returns>
        public static bool IsFrom<TNode, TEdge>(this TEdge edge, TNode fromNode)
            where TNode : IGraphNode<TNode, TEdge>
            where TEdge : IGraphEdge<TNode, TEdge>
        {
            return edge.FromNode.Equals(fromNode);
        }
        /// <summary>
        /// 判断边是否与指定节点相关联（作为起点或终点）
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="edge">要检查的边</param>
        /// <param name="node">目标节点</param>
        /// <returns>如果边与节点相关联则返回true，否则返回false</returns>
        public static bool IsPoint<TNode, TEdge>(this TEdge edge, TNode node)
            where TNode : class, IGraphNode<TNode, TEdge>
            where TEdge : class, IGraphEdge<TNode, TEdge>
        {
            return edge.IsTo(node) || edge.IsFrom(node);
        }
        /// <summary>
        /// 从边集合中查找指向目标节点的第一条边
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="edges">要查询的边集合</param>
        /// <param name="toNode">目标节点</param>
        /// <returns>如果找到则返回该边，否则返回默认值</returns>
        public static TEdge FirstTo<TNode, TEdge>(this IEnumerable<TEdge> edges, TNode toNode)
            where TNode : IGraphNode<TNode, TEdge>
            where TEdge : IGraphEdge<TNode, TEdge>
        {
            return edges.FirstOrDefault(x => x.IsTo(toNode));
        }
        /// <summary>
        /// 从边集合中查找从指定节点发出的第一条边
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="edges">要查询的边集合</param>
        /// <param name="fromNode">源节点</param>
        /// <returns>如果找到则返回第一条符合条件的边，否则返回默认值</returns>
        public static TEdge FirstFrom<TNode, TEdge>(this IEnumerable<TEdge> edges, TNode fromNode)
            where TNode : IGraphNode<TNode, TEdge>
            where TEdge : IGraphEdge<TNode, TEdge>
        {
            return edges.FirstOrDefault(x => x.IsFrom(fromNode));
        }
        /// <summary>
        /// 获取所有指向目标节点的边集合
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="edges">要查询的边集合</param>
        /// <param name="toNode">目标节点</param>
        /// <returns>返回所有指向目标节点的边集合</returns>
        public static IEnumerable<TEdge> AllTo<TNode, TEdge>(this IEnumerable<TEdge> edges, TNode toNode)
            where TNode : IGraphNode<TNode, TEdge>
            where TEdge : IGraphEdge<TNode, TEdge>
        {
            return edges.ToList().FindAll(x => x.IsTo(toNode));
        }
        /// <summary>
        /// 获取所有从指定节点发出的边集合
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="edges">要查询的边集合</param>
        /// <param name="fromNode">源节点</param>
        /// <returns>返回所有从指定节点发出的边集合</returns>
        public static IEnumerable<TEdge> AllFrom<TNode, TEdge>(this IEnumerable<TEdge> edges, TNode fromNode)
            where TNode : IGraphNode<TNode, TEdge>
            where TEdge : IGraphEdge<TNode, TEdge>
        {
            return edges.ToList().FindAll(x => x.IsFrom(fromNode));
        }
        /// <summary>
        /// 获取与指定节点相关联的另一个节点
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="edge">要查询的边</param>
        /// <param name="one">已知节点</param>
        /// <returns>返回与已知节点相关联的另一个节点，如果无关联则返回默认值</returns>
        public static TNode GetConnected<TNode, TEdge>(this TEdge edge, TNode one)
            where TNode : IGraphNode<TNode, TEdge>
            where TEdge : IGraphEdge<TNode, TEdge>
        {
            return edge.IsTo(one) ? edge.FromNode :
                edge.IsFrom(one) ? edge.ToNode : default;
        }


        /// <summary>
        /// 尝试在图中查找从起点到终点的路径（使用A*算法）
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="fromNode">路径的起始节点</param>
        /// <param name="toNode">路径的目标节点</param>
        /// <param name="pathNode">输出参数，包含找到的路径节点序列（从起点到终点顺序）</param>
        /// <param name="pathEdge">输出参数，包含找到的路径边序列（从起点到终点顺序）</param>
        /// <returns>
        /// 如果找到有效路径则返回<see langword="true"/>，否则返回<see langword="false"/>。
        /// 当返回false时，<paramref name="pathNode"/>和<paramref name="pathEdge"/>为空。
        /// </returns>
        public static bool TryGetAStartPath<TNode, TEdge>(this TNode fromNode, TNode toNode,
                out List<TNode> pathNode, out List<TEdge> pathEdge)
            where TNode : class, IGraphNode<TNode, TEdge>
            where TEdge : class, IGraphEdge<TNode, TEdge>
        {
            pathNode = new List<TNode>();
            pathEdge = new List<TEdge>();
            var openList = new List<TNode>();
            var closeList = new HashSet<TNode>();
            openList.Add(fromNode);
            while (openList.Count > 0)
            {
                var current = openList[0];
                var H = current.PathValue.Heuristic(toNode);
                var F = current.PathValue.G + H;
                for (var i = 1; i < openList.Count; i++ )
                {
                    var node = openList[i];
                    var h = node.PathValue.Heuristic(toNode);
                    var f = h + node.PathValue.G;
                    if (f < F || (f == F && h < H))
                    {
                        current = node;
                    }
                }
                openList.Remove(current);
                closeList.Add(current);
                if(current == toNode)
                {
                    while (current != null)
                    {
                        pathNode.Add(current);
                        pathEdge.Add(current.PathValue.FromEdge);
                        current = current.PathValue.Father?.Current;
                    }
                    pathNode.Reverse();
                    pathEdge.Reverse();
                    if (pathEdge.Count > 0)
                        pathEdge.RemoveAt(0);
                    foreach (var item in closeList)
                    {
                        item.PathValue.ClearPathFindData();
                    }
                    foreach (var item in openList)
                    {
                        item.PathValue.ClearPathFindData();
                    }
                    return true;
                }
                foreach (var edge in current.OutEdges)
                {
                    if (closeList.Contains(edge.ToNode))
                        continue;
                    var g = current.PathValue.G + edge.LinkWeight;
                    var notInlist = !openList.Contains(edge.ToNode);
                    if (g < edge.ToNode.PathValue.G || notInlist)
                    {
                        edge.ToNode.PathValue.G = g;
                        edge.ToNode.PathValue.Father = current.PathValue;
                        edge.ToNode.PathValue.FromEdge = edge;
                        if (notInlist)
                            openList.Add(edge.ToNode);
                    }
                }
            }
            foreach (var item in closeList)
            {
                item.PathValue.ClearPathFindData();
            }
            foreach (var item in openList)
            {
                item.PathValue.ClearPathFindData();
            }
            return false;
        }
        /// <summary>
        /// 清除路径查找数据
        /// </summary>
        /// <typeparam name="TNode">节点类型</typeparam>
        /// <typeparam name="TEdge">边类型</typeparam>
        /// <param name="pathData">要清除的路径查找数据</param>
        public static void ClearPathFindData<TNode, TEdge>(this IGraphNodePath<TNode, TEdge> pathData)
            where TNode : IGraphNode<TNode, TEdge>
            where TEdge : IGraphEdge<TNode, TEdge>
        {
            pathData.Father = null;
            pathData.FromEdge = default;
            pathData.G = 0;
        }
    }
}
