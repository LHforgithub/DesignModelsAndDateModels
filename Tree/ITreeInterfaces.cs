using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Tree
{
    /// <summary>
    /// 实现树的节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITreeNode<out T> where T : ITreeNode<T>
    {
        /// <summary>
        /// 根节点
        /// </summary>
        T Root { get; }
        /// <summary>
        /// 父节点
        /// </summary>
        T Father { get; }
        /// <summary>
        /// 子节点
        /// </summary>
        IEnumerable<T> Childrens { get; }
        /// <summary>
        /// 同父节点下其它子节点
        /// </summary>
        IEnumerable<T> Neighbours { get; }
        /// <summary>
        /// 自身所在树中层
        /// </summary>
        int TreeLevel { get; }
        /// <summary>
        /// 该节点的度
        /// </summary>
        int NodeDegree {  get; }
        /// <summary>
        /// 自身是否为根节点
        /// </summary>
        bool IsRoot { get; }
        /// <summary>
        /// 是否没有子节点
        /// </summary>
        bool IsLeaf { get; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        bool HasChild { get; }
    }
}
