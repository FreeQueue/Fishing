using UnityEngine;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using System;
using DayOwner = GameFramework.Fsm.IFsm<Fishing.Timer.DayTimer>;
namespace Fishing.Timer
{
    public abstract class DayStateBase : FsmState<DayTimer>
    {
        public abstract int dayStateID{
            get;
        }
        /// <summary>
        /// 状态初始化时调用。
        /// </summary>
        /// <param name="dayOwner">流程持有者。</param>
        protected override void OnInit(DayOwner dayOwner)
        {
            base.OnInit(dayOwner);
        }

        /// <summary>
        /// 进入状态时调用。
        /// </summary>
        /// <param name="dayOwner">流程持有者。</param>
        protected override void OnEnter(DayOwner dayOwner)
        {
            base.OnEnter(dayOwner);
            GameEntry.PlayerData.SetData(EnumIntData.DayState, dayStateID);
        }

        /// <summary>
        /// 状态轮询时调用。
        /// </summary>
        /// <param name="dayOwner">流程持有者。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected override void OnUpdate(DayOwner dayOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(dayOwner, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 离开状态时调用。
        /// </summary>
        /// <param name="dayOwner">流程持有者。</param>
        /// <param name="isShutdown">是否是关闭状态机时触发。</param>
        protected override void OnLeave(DayOwner dayOwner, bool isShutdown)
        {
            base.OnLeave(dayOwner, isShutdown);
        }

        /// <summary>
        /// 状态销毁时调用。
        /// </summary>
        /// <param name="dayOwner">流程持有者。</param>
        protected override void OnDestroy(DayOwner dayOwner)
        {
            base.OnDestroy(dayOwner);
        }
    }
}