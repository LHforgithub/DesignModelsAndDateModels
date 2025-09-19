using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// 单例模式 延迟+双锁+反射实现，记得封闭构造函数以避免直接生成
    /// </summary>
    /// <typeparam name="T">具体的单例类</typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        /// <summary>
        /// 单例类的具体构造函数。通过私有约束其不能直接被构造为实例。
        /// </summary>
        /// <remarks>
        /// <see langword="不应该通过构造函数创建单例类型的实例！" />
        /// </remarks>
        protected Singleton() { }
        /// <summary>
        /// 类型单例
        /// </summary>
        public static T Instance => Nested.Instance;
        /// <summary>
        /// 是否已初始化
        /// </summary>
        public static bool IsInitiate => Nested.Instance != default;
        ///内部类实现延迟创建
        class Nested
        {
            static Nested() { }
            static readonly object initLock = new();
            internal static T Instance
            {
                get
                {
                    //双锁 线程安全
                    if (_instance == null)
                    {
                        lock (initLock)
                        {
                            if(_instance == null)
                            {
                                //反射构造类型实例
                                //由于一个单例只会被调用一次，性能开销并不会很大。
                                //确保有无参构造函数
                                //Type t = typeof(T);
                                //var ctors = t.GetConstructors(
                                //    ).Where(ctor => ctor.GetParameters().Length < 1).ToArray();
                                //if (ctors.Length < 1)
                                //    throw new InvalidOperationException();
                                //_instance = (T)ctors[0].Invoke(null);
                                _instance = (T)Activator.CreateInstance(typeof(T),
                                    bindingAttr: BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                                    null, null, null);
                            }
                        }
                    }
                    return _instance;
                }
            }
            internal static T _instance;
        }
    }
}
