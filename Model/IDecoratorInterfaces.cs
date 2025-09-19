using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// 装饰器管理器接口，负责维护装饰器链并协调装饰执行顺序
    /// </summary>
    /// <typeparam name="T">被装饰对象类型</typeparam>
    /// <typeparam name="U">具体装饰器类型，需继承自<typeparamref name="T"/>并实现<see cref="IDecorator{T, U}"/>接口</typeparam>
    /// <remarks>
    /// 实现本接口的类应自行处理装饰器执行顺序的优先级问题
    /// </remarks>
    public interface IDecoratorManager<T, U>
        where U : class, T, IDecorator<T, U>
    {
        /// <summary>
        /// 获取原始待装饰对象（非装饰后的值）
        /// </summary>
        /// <value>未经过任何装饰处理的原始对象</value>
        T OriginalDecoratableValue { get; }
        
        /// <summary>
        /// 设置原始待装饰对象
        /// </summary>
        /// <param name="obj">待装饰的原始对象</param>
        /// <exception cref="ArgumentNullException">当obj为null时抛出</exception>
        void SetOriginalDecoratableValue(T obj);
        
        /// <summary>
        /// 获取当前装饰链处理后的最终对象
        /// </summary>
        /// <value>应用所有装饰器后的结果对象</value>
        T DecoratedValue { get; }
        
        /// <summary>
        /// 向装饰链添加新装饰器
        /// </summary>
        /// <param name="decorator">要添加的装饰器实例</param>
        void AddDecorator(U decorator);
        
        /// <summary>
        /// 从装饰链移除指定装饰器
        /// </summary>
        /// <param name="decorator">要移除的装饰器实例</param>
        void RemoveDecorator(U decorator);
    }
    /// <summary>
    /// 装饰器定义
    /// </summary>
    /// <typeparam name="T">被装饰对象基类型</typeparam>
    /// <typeparam name="U">装饰器具体类型，需继承自<typeparamref name="T"/></typeparam>
    /// <remarks>
    /// 装饰器实现类应确保装饰操作不会破坏原对象的行为
    /// </remarks>
    public interface IDecorator<T, U> where U : class, T, IDecorator<T, U>
    {
        /// <summary>
        /// 获取当前装饰器正在装饰的目标对象
        /// </summary>
        /// <value>被装饰的原始对象或前序装饰结果</value>
        /// <remarks>建议实际实现时此值为原始待装饰对象，通过管理器使用散列而非使用链表形式。</remarks>
        T DecorateObject { get; }
        
        /// <summary>
        /// 设置装饰操作的目标对象
        /// </summary>
        /// <param name="decorateObject">要装饰的对象实例</param>
        void SetDecotateObject(T decorateObject);
    }
}
