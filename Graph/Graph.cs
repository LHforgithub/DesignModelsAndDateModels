using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Graph
{
    /// <summary>
    /// 表示图数据结构中的节点
    /// </summary>
    /// <typeparam name="T">图节点的类型</typeparam>
    /// <typeparam name="U">图边的类型</typeparam>
    public interface IGraphNode<T, U> where T : IGraphNode<T, U> where U : IGraphEdge<T, U>
    {
        /// <summary>
        /// 获取指向此节点的所有入边的集合
        /// </summary>
        HashSet<U> InEdges { get; }

        /// <summary>
        /// 获取从此节点出发的所有出边的集合
        /// </summary>
        HashSet<U> OutEdges { get; }
        /// <summary>
        /// 创建并添加一条从当前节点到指定节点的新边
        /// </summary>
        /// <param name="otherNode">要连接的目标节点</param>
        /// <param name="weight">新边的权重</param>
        /// <returns>新创建的边</returns>
        U AddNewEdgeTo(T otherNode, uint weight);
        /// <summary>
        /// 尝试移除当前节点到指定节点的所有边
        /// </summary>
        /// <param name="otherNode">目标节点</param>
        /// <param name="edges">包含被移除边的输出参数</param>
        /// <returns>如果移除了任何边，则返回<see langword="true"/>；否则，<see langword="false"/></returns>
        bool TryRemoveAllEdgeTo(T otherNode, out List<U> edges);

        /// <summary>
        /// 尝试移除从指定节点到当前节点的所有边
        /// </summary>
        /// <param name="otherNode">源节点</param>
        /// <param name="edges">包含被移除边的输出参数</param>
        /// <returns>如果移除了任何边，则返回<see langword="true"/>；否则，<see langword="false"/></returns>
        bool TryRemoveAllEdgeFrom(T otherNode, out List<U> edges);
        /// <summary>
        /// 添加一条已存在的边到当前节点
        /// </summary>
        /// <param name="edge">要添加的边</param>
        /// <returns>如果边添加成功，则返回<see langword="true"/>；否则，<see langword="false"/></returns>
        bool AddNewEdge(U edge);
        /// <summary>
        /// 从当前节点移除指定的边
        /// </summary>
        /// <param name="edge">要移除的边</param>
        /// <returns>如果边移除成功，则返回<see langword="true"/>；否则，<see langword="false"/></returns>
        bool RemoveEdge(U edge);
        /// <summary>
        /// 移除当前节点的所有边
        /// </summary>
        void RemoveAllEdges();
        /// <summary>
        /// 获取与此节点关联的路径信息，用于路径查找算法
        /// </summary>
        IGraphNodePath<T, U> PathValue { get; }
    }
    /// <summary>
    /// 表示图中连接两个节点的边
    /// </summary>
    /// <typeparam name="T">图节点的类型</typeparam>
    /// <typeparam name="U">图边的类型</typeparam>
    public interface IGraphEdge<out T, out U> where T : IGraphNode<T, U> where U : IGraphEdge<T, U>
    {
        /// <summary>
        /// 获取此边的起始节点
        /// </summary>
        T FromNode { get; }
        /// <summary>
        /// 获取此边的指向节点
        /// </summary>
        T ToNode { get; }
        /// <summary>
        /// 获取此边的权重
        /// </summary>
        uint LinkWeight { get; }
    }
    /// <summary>
    /// 表示路径查找过程中图节点的路径信息
    /// </summary>
    /// <typeparam name="T">图节点的类型</typeparam>
    /// <typeparam name="U">图边的类型</typeparam>
    public interface IGraphNodePath<T, U> where T : IGraphNode<T, U> where U : IGraphEdge<T, U>
    {
        /// <summary>
        /// 获取或设置路径中的父节点
         /// </summary>
         /// <remarks>此值由寻路算法自动设置，请勿使用</remarks>
        IGraphNodePath<T, U> Father { get; set; }
        /// <summary>
        /// 获取或设置路径中指向当前节点的边
        /// </summary>
        /// <remarks>此值由寻路算法自动设置，请勿使用</remarks>
        U FromEdge { get; set; }
        /// <summary>
        /// 获取或设置从起始节点到当前节点的实际成本
        /// </summary>
        /// <remarks>此值由寻路算法自动设置，请勿使用</remarks>
        uint G { get; set; }
        /// <summary>
        /// 获取当前节点
        /// </summary>
        /// <remarks>此值必须已指向一个非空的节点实例</remarks>
        T Current { get; }
        /// <summary>
        /// 计算从当前节点到另一个节点的启发式值（估计成本）
        /// </summary>
        /// <param name="otherNode">目标节点</param>
        /// <returns>启发式值</returns>
        uint Heuristic(T otherNode);
    }

    //public class Graph<TNode, TEdge> where TNode : IGraphNode where TEdge : IGraphEdge
    //{
    //    internal HashSet<TNode> Nodes { get; } = new HashSet<TNode>();
    //    internal HashSet<TEdge> Edges { get; } = new HashSet<TEdge>();
    //}
}
