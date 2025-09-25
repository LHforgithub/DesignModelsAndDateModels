using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// 组合模式接口，定义类的组件操作
    /// </summary>
    /// <typeparam name="T">组件类型，必须实现<see cref="IComponentComposite"/>接口</typeparam>
    public interface IComponent<T> where T : IComponentComposite
    {
        /// <summary>
        /// 获取当前组件包含的所有子组件
        /// </summary>
        /// <value>子组件列表</value>
        List<T> Components { get; }
        /// <summary>
        /// 添加子组件
        /// </summary>
        /// <param name="component">要添加的组件实例</param>
        /// <returns>添加的组件的组合内唯一标识符</returns>
        int AddComponent(T component);
        /// <summary>
        /// 移除指定子组件
        /// </summary>
        /// <param name="component">要移除的组件实例</param>
        /// <returns>是否成功移除了组件</returns>
        bool RemoveComponent(T component);
        /// <summary>
        /// 尝试移除指定类型的子组件并抛出移除的对象
        /// </summary>
        /// <typeparam name="U">目标组件类型</typeparam>
        /// <param name="removed">如果成功移除组件，返回其实例；否则，<see langword="default"/></param>
        /// <returns>是否成功移除了组件</returns>
        bool TryRemoveComponent<U>(out U removed) where U : T;
        /// <summary>
        /// 尝试获取指定类型的组件
        /// </summary>
        /// <typeparam name="U">目标组件类型</typeparam>
        /// <param name="component">找到的组件实例</param>
        /// <returns>是否找到匹配组件</returns>
        bool TryGetComponent<U>(out U component) where U : T;
        /// <summary>
        /// 按名称获取指定类型的组件
        /// </summary>
        /// <param name="name">组件名称</param>
        /// <returns>找到的组件实例</returns>
        T GetComponent(string name);
        /// <summary>
        /// 按标识符获取指定类型的组件
        /// </summary>
        /// <param name="id">组件标识符</param>
        /// <returns>找到的组件实例</returns>
        T GetComponent(int id);
    }

    /// <summary>
    /// 组件基础接口，定义组合模式中的节点
    /// </summary>
    public interface IComponentComposite
    {
        /// <summary>
        /// 获取组件唯一标识名称
        /// </summary>
        /// <value>组件名称，应当由自身而非组合赋值</value>
        string ComponentName { get; }
        /// <summary>
        /// 获取组件唯一标识符
        /// </summary>
        /// <value>组件标识符，应当由组合在加入组合时赋值而非自身赋值</value>
        int ComponentID { get; }
        /// <summary>
        /// 设置组件唯一标识符，由组合调用
        /// </summary>
        /// <param name="componentID">设置的标识符值</param>
        void SetComponentID(int componentID);
    }
    /// <summary>
    /// 组件基础接口，定义组合模式中的节点
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    public interface IComponentComposite<T, U> : IComponentComposite
        where T : IComponent<U>
        where U : IComponentComposite<T, U>
    {
        /// <summary>
        /// 获取当前组件的父级组合
        /// </summary>
        T TargetComponent { get; }
        /// <summary>
        /// 设置当前组件的父级组合
        /// </summary>
        /// <param name="target"></param>
        void SetTargetComponent(T target);
    }
}
