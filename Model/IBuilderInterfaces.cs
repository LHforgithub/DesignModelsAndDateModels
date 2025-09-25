using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// 构建器接口，定义对象构建流程
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// 构建目标对象
        /// </summary>
        /// <returns>构建对象</returns>
        object Build();
        /// <summary>
        /// 获取当前注册的所有构建函数集合
        /// </summary>
        /// <value>构建函数哈希集合</value>
        IEnumerable<IBuildingFunction> BuildingFuncs { get; }
        /// <summary>
        /// 添加构建函数到组装线中
        /// </summary>
        /// <param name="buildingFunction"></param>
        void AddBuildFunc(IBuildingFunction buildingFunction);
        /// <summary>
        /// 移除构建函数从组装线中
        /// </summary>
        /// <param name="buildingFunction"></param>
        void RemoveBuildFunc(IBuildingFunction buildingFunction);
    }
    /// <summary>
    /// 构建器接口，定义对象构建流程和组装线管理
    /// </summary>
    /// <typeparam name="T">要构建的目标对象类型</typeparam>
    /// <typeparam name="U">构建函数类型，继承<see cref="IBuildingFunction{T}"/>接口</typeparam>
    public interface IBuilder<T, U> : IBuilder
        where T : class
        where U : class, IBuildingFunction<T>
    {
        /// <summary>
        /// 获取预处理构建器实例
        /// </summary>
        /// <value>负责对象初始化阶段的构建器</value>
        IPreBuilder<T> PreBuilder { get; }
        /// <summary>
        /// 获取当前注册的所有构建函数集合
        /// </summary>
        /// <value>构建函数哈希集合</value>
        new HashSet<U> BuildingFuncs { get; }
        /// <summary>
        /// 执行完整构建流程
        /// </summary>
        /// <returns>构建完成的目标对象</returns>
        new T Build();

        /// <summary>
        /// 通过组装线顺序执行所有构建函数
        /// </summary>
        /// <param name="originalValue">初始对象</param>
        /// <returns>经过所有构建函数处理后的对象</returns>
        T AssemblyLine(T originalValue);

        /// <summary>
        /// 注册新的构建函数
        /// </summary>
        /// <param name="buildFunc">要添加的构建函数实例</param>
        void AddBuildFunc(U buildFunc);

        /// <summary>
        /// 注销指定的构建函数
        /// </summary>
        /// <param name="buildFunc">要移除的构建函数实例</param>
        void RemoveBuildFunc(U buildFunc);

        /// <summary>
        /// 获取按优先级排序的构建函数数组
        /// </summary>
        /// <returns>排序后的构建函数数组</returns>
        U[] OrderedBuildFuncs();
    }
    /// <summary>
    /// 预处理构建器接口，负责目标对象的初始化阶段
    /// </summary>
    /// <typeparam name="T">要构建的目标对象类型</typeparam>
    public interface IPreBuilder<T> where T : class
    {
        /// <summary>
        /// 执行对象预处理构建，以通过无参函数获得实例
        /// </summary>
        /// <returns>初步构建的对象实例</returns>
        T PreBuild();
    }
    /// <summary>
    /// 构建函数接口，定义对象构建的单个步骤
    /// </summary>
    public interface IBuildingFunction
    {

        /// <summary>
        /// 获取构建函数执行优先级
        /// </summary>
        /// <remarks>实际实现通过<see cref="IComparable{T}"/>接口和<see cref="IBuilder{T,U}.OrderedBuildFuncs"/>实现。</remarks>
        int Priority { get; }
        /// <summary>
        /// 设置构建函数执行优先级
        /// </summary>
        /// <param name="priority">新的优先级值</param>
        void SetDecoratorPriority(int priority);
    }
    /// <summary>
    /// 构建函数接口，定义对象构建的单个步骤
    /// </summary>
    /// <typeparam name="T">要构建的目标对象类型</typeparam>
    public interface IBuildingFunction<T> : IBuildingFunction, IComparable<IBuildingFunction<T>> where T : class
    {
        /// <summary>
        /// 执行单个构建步骤
        /// </summary>
        /// <param name="originalValue">上一步构建结果</param>
        /// <returns>经过当前构建步骤处理后的对象</returns>
        T Build(T originalValue);
    }
}
