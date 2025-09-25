using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// 虚拟工厂，用于创建其它工厂，并保证每个工厂只提供一个实例。
    /// </summary>
    /// <typeparam name="TFactory">创建的工厂的抽象类型</typeparam>
    public class FactoryMaker<TFactory> where TFactory : class, IFactory
    {
        private HashSet<TFactory> Factorys { get; } = new HashSet<TFactory>();
        /// <summary>
        /// 创建工厂类实例，并加入哈希表中，后续调用时直接返回哈希表中已创建工厂对象。
        /// </summary>
        /// <typeparam name="U">类类型</typeparam>
        /// <param name="args">构造函数参数</param>
        /// <returns>类实例</returns>
        /// <remarks>因为是通过反射创建，需要注意输入的参数和构造函数相匹配。</remarks>
        /// <inheritdoc cref="Activator.CreateInstance(Type, BindingFlags, Binder, object[], System.Globalization.CultureInfo)"/>/>
        public virtual U GetFactory<U>(params object[] args) where U : class, TFactory
        {
            if (Factorys.FirstOrDefault(x => x is U) is U result)
            {
                return result;
            }
            args ??= Array.Empty<object>();
            var newObj = (U)Activator.CreateInstance(typeof(U),
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                null, args, null);
            Factorys.Add(newObj);
            return newObj;
        }

        /// <inheritdoc cref="GetFactory{U}"/>
        /// <param name="type">工厂类型</param>
        public virtual TFactory GetFactory(Type type, params object[] args)
        {
            if (type == null) return default;
            if (Factorys.FirstOrDefault(x => x.GetType() == type) is TFactory result)
            {
                return result;
            }
            args ??= Array.Empty<object>();
            var newObj = (TFactory)Activator.CreateInstance(type,
                bindingAttr: BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                null, args, null);
            Factorys.Add(newObj);
            return newObj;
        }
        public virtual TFactory GetFactory(string typeName, params object[] args)
        {
            return GetFactory(Type.GetType(typeName, false, false), args);
        }
        public IEnumerable<TFactory> GetAll() => Factorys;
    }
}
