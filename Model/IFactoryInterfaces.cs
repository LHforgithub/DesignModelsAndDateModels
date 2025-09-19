using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// -不要直接继承此接口，工厂应当继承<see cref="IFactory{T}"/>接口。
    /// </summary>
    public interface IFactory { }
    /// <summary>
    /// 工厂接口，实现创建产品功能
    /// </summary>
    /// <typeparam name="T">创建的产品类型</typeparam>
    /// <typeparam name="U">该工厂类型</typeparam>
    public interface IFactory<T, U> : IFactory where T : class, IProduction<U> where U : class, IFactory<T, U>
    {
        /// <summary>
        /// 通过参数创建产品
        /// </summary>
        /// <param name="args">输入的不定参数</param>
        /// <returns>创建的产品实例</returns>
        T Make(params object[] args);
    }
    /// <summary>
    /// -不要直接继承此接口，产品应当继承<see cref="IProduction{T}"/>接口
    /// </summary>
    public interface IProduction { }
    /// <summary>
    /// 产品接口，具体的产品实例
    /// </summary>
    /// <typeparam name="T">创建此产品的工厂</typeparam>
    public interface IProduction<T> : IProduction where T : class, IFactory
    {
        /// <summary>
        /// 创建此产品的工厂实例
        /// </summary>
        T Factory { get; }
        /// <summary>
        /// 预留受工厂创建时接口
        /// </summary>
        /// <param name="factory"></param>
        void OnCreatedByFactory(T factory);
    }
}
