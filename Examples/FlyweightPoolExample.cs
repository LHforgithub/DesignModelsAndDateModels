using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDM.Model;
namespace MDM.Examples
{
    /// <summary>
    /// 示例类，展示如何使用享元模式池
    /// </summary>
    internal sealed class FlyweightPoolExample
    {
        /// <summary>
        /// 构造函数，初始化享元池
        /// </summary>
        public FlyweightPoolExample()
        {
            FlyweightPool = new FlyweightPool<FlyweightClass>(() => new FlyweightClass(this));
        }
        
        /// <summary>
        /// 享元对象池
        /// </summary>
        public FlyweightPool<FlyweightClass> FlyweightPool { get; set; }
        
        /// <summary>
        /// 当享元对象使用结束时调用
        /// </summary>
        /// <param name="instance">要回收的享元实例</param>
        public void CallEnd(FlyweightClass instance)
        {
            FlyweightPool.Disposed(instance);
        }
    }

    /// <summary>
    /// 享元类，表示可复用的轻量级对象
    /// </summary>
    internal sealed class FlyweightClass
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Factory">创建此对象的工厂</param>
        public FlyweightClass(FlyweightPoolExample Factory)
        {
            factory = Factory;
        }
        
        private readonly FlyweightPoolExample factory;
        
        /// <summary>
        /// 当对象使用结束时调用，将对象回收到池中
        /// </summary>
        public void CallEnd()
        {
            factory.CallEnd(this);
        }
    }
}
