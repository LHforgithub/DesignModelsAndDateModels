using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Model
{
    /// <summary>
    /// 状态机接口，管理状态转换和状态更新
    /// </summary>
    /// <typeparam name="T">状态类型，必须实现<see cref="IState{T, U}"/>接口</typeparam>
    /// <typeparam name="U">状态事件类型，必须实现<see cref="IStateEvent"/>接口</typeparam>
    public interface IStateMachine<T, U> where T : IState<T, U> where U : IStateEvent
    {
        /// <summary>
        /// 获取当前状态
        /// </summary>
        /// <value>当前活跃状态实例</value>
        T CurrentState { get; }
        
        /// <summary>
        /// 获取状态转换字典（事件到状态的映射）
        /// </summary>
        /// <value>状态转换规则字典</value>
        Dictionary<U, T> EventStateDic { get; }
        /// <summary>
        /// 获取状态转换字典（状态到事件的映射）
        /// </summary>
        /// <value>状态转换规则字典</value>
        Dictionary<T, U> StateEventDic { get; }
        
        /// <summary>
        /// 根据事件转换到新状态
        /// </summary>
        /// <param name="stateEvent">触发状态转换的事件</param>
        void ChangeState(U stateEvent);
        /// <summary>
        /// 添加新状态至状态机，若已存在对应状态，仅更新状态所对应事件。
        /// </summary>
        /// <param name="state">新状态</param>
        /// <param name="stateEvent">新状态更新事件</param>
        void AddStateOrUpdateStateEvent(T state, U stateEvent);
        /// <summary>
        /// 移除某状态
        /// </summary>
        /// <param name="state">要移除的状态</param>
        /// <returns>是否移除成功</returns>
        bool RemoveState(T state);
        /// <summary>
        /// 移除某状态，通过对应的状态事件
        /// </summary>
        /// <param name="stateEvent">移除的状态事件</param>
        /// <returns>是否移除成功</returns>
        bool RemoveState(U stateEvent);
        /// <summary>
        /// 通过状态事件获取对应状态
        /// </summary>
        /// <param name="stateEvent">键</param>
        /// <param name="state">值</param>
        /// <returns>是否成功获取</returns>
        bool TryGetState(U stateEvent, out T state);
        /// <summary>
        /// 通过状态获取对应状态事件
        /// </summary>
        /// <param name="state">键</param>
        /// <param name="eventState">值</param>
        /// <returns>是否成功获取</returns>
        bool TryGetEvent(T state, out U eventState);
        /// <summary>
        /// 更新当前状态
        /// </summary>
        void UpdateCurrentState();
    }
    /// <summary>
    /// 状态事件接口，定义状态转换的触发条件
    /// </summary>
    public interface IStateEvent
    {
    }
    /// <summary>
    /// 状态行为接口，定义状态的生命周期方法
    /// </summary>
    /// <typeparam name="T">状态类型</typeparam>
    /// <typeparam name="U">状态事件类型</typeparam>
    public interface IState<T, U> where T : IState<T, U> where U : IStateEvent
    {
        /// <summary>
        /// 进入状态时调用的方法
        /// </summary>
        /// <param name="evt">状态对应的事件</param>
        /// <param name="lastState">进入此状态前，上一个状态实例</param>
        void Entry(U evt, T lastState);

        /// <summary>
        /// 状态更新时调用的方法
        /// </summary>
        /// <param name="evt">状态对应的事件</param>
        void Update(U evt);

        /// <summary>
        /// 退出状态时调用的方法
        /// </summary>
        /// <param name="evt">状态对应的事件</param>
        /// <param name="nextState">即将进入的下一个状态实例</param>
        void Exit(U evt, T nextState);
    }
}
