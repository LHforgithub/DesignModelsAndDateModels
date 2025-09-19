using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Tree
{
    /// <summary>
    /// 树型结构方法拓展
    /// </summary>
    public static class TreeExtensions
    {
        /// <summary>
        /// 获取从节点到根的路径（包含自身和节点在内的顺序经过的节点集合）
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="node">节点</param>
        /// <returns>路径中按经过顺序组成的节点集合</returns>
        public static IEnumerable<T> GetPathToRoot<T>(this T node)
            where T : ITreeNode<T>
        {
            var start = node;
            while (start != null)
            {
                yield return start;
                start = start.Father;
            }
        } 
        /// <summary>
        /// 获取从根到节点的路径（包含自身和节点在内的顺序经过的节点集合）
        /// </summary>
        /// <inheritdoc cref="TreeExtensions.GetPathToRoot{T}(T)"/>
        public static IEnumerable<T> GetPathFromRoot<T>(this T node)
            where T : ITreeNode<T> => GetPathToRoot(node).Reverse();
        /// <summary>
        /// 判断节点是否是指定节点的先祖节点
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="node">指定节点</param>
        /// <param name="target">判断节点</param>
        /// <returns>判断结果</returns>
        public static bool IsFatherOf<T>(this T node, T target)
            where T : class, ITreeNode<T>
        {
            if (node.Root != target.Root) return false;
            return GetPathToRoot(node).SingleOrDefault(n => n.Equals(node)) != null;
        }
        /// <summary>
        /// 判断节点是否是指定节点的后代节点
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="node">指定节点</param>
        /// <param name="target">判断节点</param>
        /// <returns>判断结果</returns>
        public static bool IsChildOf<T>(this T node, T target)
            where T : class, ITreeNode<T>
        {
            return IsFatherOf(target, node);
        }
        /// <summary>
        /// 获取节点与指定节点的最近公共祖先节点
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="node">待查找的节点</param>
        /// <param name="target">指定节点</param>
        /// <returns>最近的公共祖先节点</returns>
        public static T GetNearestFather<T>(this T node, T target)
            where T : class, ITreeNode<T>
        {
            if (node.Root != target.Root) return default;
            return node.GetPathToRoot().Intersect(target.GetPathToRoot()).OrderByDescending(no => no.TreeLevel).FirstOrDefault();
        }
        /// <summary>
        /// 获取子孙数据（深度优先，先序）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="node">根</param>
        /// <param name="predicate">子孙筛选条件</param>
        /// <returns>筛选的子孙</returns>
        public static IEnumerable<T> GetChildsDFS<T>(this T node,
            Func<T, bool> predicate = null)
            where T : ITreeNode<T>
        {
            var children = predicate == null ? node.Childrens : node.Childrens.Where(predicate);
            var stack = new Stack<T>(children.Reverse());
            while (stack.Count > 0)
            {
                var next = stack.Pop();
                yield return next;
                children = predicate == null ? next.Childrens : next.Childrens.Where(predicate);
                foreach (var child in children.Reverse())
                {
                    stack.Push(child);
                }
            }
        }
        /// <summary>
        /// 获取子孙数据（广度优先）
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="node">根</param>
        /// <param name="predicate">子孙筛选条件</param>
        /// <returns>筛选的子孙</returns>
        public static IEnumerable<T> GetChildBFS<T>(this T node,
            Func<T, bool> predicate = null)
            where T : ITreeNode<T>
        {
            predicate = predicate ?? (t => true);
            var queue = new Queue<T>(node.Childrens.Where(predicate));
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                yield return next;
                foreach (var child in next.Childrens.Where(predicate))
                {
                    queue.Enqueue(child);
                }
            }
        }

        /// <summary>
        /// 获取树中某层所有满足条件的节点，先序
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="node">树中任意一个节点</param>
        /// <param name="level">要获取的树层级</param>
        /// <param name="predicate">筛选条件</param>
        /// <returns>该层级中所有节点</returns>
        public static IEnumerable<T> GetNodeWithLevel<T>(this T node, int level,
            Func<T, bool> predicate = null)
            where T : ITreeNode<T>
        {
            if (level < 0) yield break;
            predicate ??= (t => true);
            var stack = new Stack<T>() { };
            stack.Push(node.Root);
            while (stack.Count > 0)
            {
                var next = stack.Pop();
                if(next.TreeLevel == level && predicate(next))
                    yield return next;
                else
                {
                    foreach(var n in next.Childrens.Reverse())
                    {
                        stack.Push(n);
                    }
                }
            }
        }
        /// <summary>
        /// 获取相同父节点下的其它子节点，不包含自身
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="node">目标节点</param>
        /// <param name="predicate">筛选方法</param>
        /// <returns>邻居节点</returns>
        public static IEnumerable<T> GetNeighbours<T>(this T node,
            Func<T, bool> predicate = null)
            where T : ITreeNode<T>
        {
            return node.Neighbours.Where(predicate);
        }
        /// <summary>
        /// 获取目标节点下层级最大的节点
        /// </summary>
        /// <typeparam name="T">节点类型</typeparam>
        /// <param name="node">目标节点</param>
        /// <returns>找寻到的层级最大的节点</returns>
        public static T GetDeepthChild<T>(this T node)
            where T : ITreeNode<T>
        {
            var stack = new Stack<T>(node.Childrens.Reverse());
            T deepth = default;
            int Level = 0;
            while (stack.Count > 0)
            {
                var next = stack.Pop();
                if(next.HasChild)
                {
                    foreach(var n in next.Childrens.Reverse())
                    {
                        stack.Push(n);
                    }
                }
                else if (next.TreeLevel > Level)
                {
                    Level = next.TreeLevel;
                    deepth = next;
                }
            }
            return deepth;
        }
    }
}
