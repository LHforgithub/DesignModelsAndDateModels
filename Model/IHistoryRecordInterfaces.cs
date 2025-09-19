using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// 表示历史记录步骤的接口
    /// </summary>
    /// <typeparam name="T">实现该接口的类型。</typeparam>    
    public interface IHistoryRecord<T> where T : IHistoryRecord<T>
    {
        /// <summary>
        /// 创建当前记录的克隆副本
        /// </summary>
        /// <returns>当前实例的克隆（新的引用实例）</returns>
        T CloneRecord();
    }
    /// <summary>
    /// 历史记录管理器接口，提供历史记录的导航和操作功能
    /// </summary>
    /// <typeparam name="T">历史记录类型</typeparam>
    public interface IHistoryRecordManager<T> where T : IHistoryRecord<T>
    {
        /// <summary>
        /// 获取当前历史记录步骤
        /// </summary>
        T CurrentRecordStep { get; }
        /// <summary>
        /// 添加新的历史记录步骤到记录栈中
        /// </summary>
        /// <param name="record">要添加的历史记录</param>
        void AddNextRecord(T record);
        /// <summary>
        /// 撤销到上一个历史记录步骤
        /// </summary>
        void Undo();
         /// <summary>
        /// 重做到下一个历史记录步骤
        /// </summary>
        void Redo();
        /// <summary>
        /// 清除所有历史记录
        /// </summary>
        void ClearRecords();
    }
    /// <summary>
    /// 可记录历史变化的对象接口，定义了创建和应用历史记录的方法
    /// </summary>
    /// <typeparam name="T">历史记录类型</typeparam>
    public interface IHistoryRecordable<T> where T : IHistoryRecord<T>
    {
        /// <summary>
        /// 创建当前状态的历史记录快照
        /// </summary>
        /// <returns>包含当前状态的历史记录对象</returns>
        T CreateHistoryRecord();
        /// <summary>
        /// 应用历史记录到当前对象，恢复历史状态
        /// </summary>
        /// <param name="record">要应用的历史记录</param>
        void ApplyHistoryRecord(T record);
    }
}
