using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDM.Model;

namespace MDM.Examples
{
    /// <summary>
    /// 工厂模式示例类，实现<see cref="IFactory{T,U}"/>接口
    /// </summary>
    internal sealed class FactoryExample : IFactory<ProductExample, FactoryExample>
    {
        /// <summary>
        /// 创建静态的创建FactoryExample实例的超级工厂，以便于方法调用
        /// </summary>
        public static FactoryMaker<FactoryExample> Creator { get; } = new FactoryMaker<FactoryExample>();
        /// <summary>
        /// 创建产品实例
        /// </summary>
        /// <param name="args">创建产品时需要的参数</param>
        /// <returns>新创建的产品实例</returns>
        public ProductExample Make(params object[] args)
        {
            var product = new ProductExample();
            product.OnCreatedByFactory(this);
            return product;
        }
    }

    /// <summary>
    /// 产品示例类，实现<see cref="IProduction"/>接口
    /// </summary>
    internal sealed class ProductExample : IProduction
    {
        /// <summary>
        /// 获取创建此产品的工厂实例
        /// </summary>
        public IFactory Factory { get; private set; }

        /// <summary>
        /// 当产品被工厂创建时调用
        /// </summary>
        /// <param name="factory">创建此产品的工厂实例</param>
        public void OnCreatedByFactory(IFactory factory)
        {
            Factory = factory;
        }
    }
}
