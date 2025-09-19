using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDM.Model;
namespace MDM.Examples
{
    /// <summary>
    /// 这是一个用于单例模式的写法例子
    /// </summary>
    internal sealed class SingletonExample : Singleton<SingletonExample>
    {
        /// <inheritdoc cref="Singleton{T}.Singleton"/>
        private SingletonExample() : base() { }
        /// <summary></summary>
        public string GetTestString()
        {
            return $"Type: {GetType()}, Hash: {GetHashCode()}";
        }
    }
}
