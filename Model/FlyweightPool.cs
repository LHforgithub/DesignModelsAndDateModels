using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// 享元对象池
    /// </summary>
    /// <typeparam name="T">要管理的享元对象</typeparam>
    public sealed class FlyweightPool<T> where T : class
    {
        private FlyweightPool() { }
        /// <summary>
        /// 构造新的享元对象池
        /// </summary>
        /// <param name="createNewObject">在请求获取享元对象时，若所有对象均在使用，调用此函数实现创建新的享元对象。</param>
        /// <param name="isValid">检查享元对象合法性，用于重写了享元对象的可用性时使用</param>
        public FlyweightPool(Func<T> createNewObject, Func<T, bool> isValid = default)
        {
            ObjectPool= new HashSet<T>();
            UsingPool = new HashSet<T>();;
            CreateNewObject = createNewObject;
            IsValid = isValid;
        }
        private HashSet<T> ObjectPool { get; set; }
        /// <summary>
        /// 空闲状态的享元对象数量
        /// </summary>
        public int FreeCount => ObjectPool.Count;
        private HashSet<T> UsingPool { get; set; }
        /// <summary>
        /// 使用状态的享元对象数量
        /// </summary>
        public int UsingCount => UsingPool.Count;
        private Func<T> CreateNewObject { get; set; }
        private Func<T, bool> IsValid { get; set; }
        /// <summary>
        /// 获取享元对象
        /// </summary>
        /// <param name="Filter">额外筛选满足条件的享元对象才会被返回</param>
        /// <returns></returns>
        public T Out(Predicate<T> Filter = null)
        {
            ValidCheck();
            if (ObjectPool.Count < 1)
            {
                return CreateNew(Filter, true);
            }
            T result = default;
            if(Filter == null)
            {
                result = ObjectPool.FirstOrDefault();
            }
            else
            {
                result = ObjectPool.FirstOrDefault(x => Filter(x));
            }
            if (result == default)
            {
                return CreateNew(Filter, true);
            }
            ObjectPool.Remove(result);
            UsingPool.Add(result);
            return result;
        }
        /// <summary>
        /// 释放被使用的享元对象
        /// </summary>
        /// <param name="obj"></param>
        public void Disposed(T obj)
        {
            if (IsValid != default && !IsValid(obj)) return;
            UsingPool.Remove(obj);
            ObjectPool.Add(obj);
            return;
        }
        private T CreateNew(Predicate<T> Filter, bool used)
        {
            T result = default;
            if (CreateNewObject == default)
            {
                return default;
            }
            result = CreateNewObject();
            if (Filter != null && !Filter(result))
            {
                return default;
            }
            if (used) UsingPool.Add(result);
            else ObjectPool.Add(result);
            return result;
        }
        /// <summary>
        /// 检查享元对象合法性
        /// </summary>
        public void ValidCheck()
        {
            if (IsValid == default) return;
            var all = ObjectPool.Where(x=> !IsValid(x)).ToList();
            all.ForEach(x => ObjectPool.Remove(x));
        }
    }

}
