using GameFramework;
using GameFramework.Fsm;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityGameFramework.Runtime;
using GameFramework.Event;
namespace Fishing.Timer
{
    public class DayTimer
    {
        private IFsm<DayTimer> m_DayStateFsm;
        private Dictionary<int, DayStateBase> m_DayStateBaseDic;
        /// <summary>
        /// 获取当前状态。
        /// </summary>
        public DayStateBase CurrentDayState
        {
            get
            {
                if (m_DayStateFsm == null)
                {
                    throw new GameFrameworkException("You must initialize DayTimer first.");
                }
                return (DayStateBase)m_DayStateFsm.CurrentState;
            }
        }

        /// <summary>
        /// 获取当前流程持续时间。
        /// </summary>
        public float CurrentDayStateTime
        {
            get
            {
                if (m_DayStateFsm == null)
                {
                    throw new GameFrameworkException("You must initialize DayTimer first.");
                }
                return m_DayStateFsm.CurrentStateTime;
            }
        }

        public void StartDayTimer()
        {
            m_DayStateFsm = GameEntry.Fsm.CreateFsm(this, m_DayStateBaseDic.Values.ToArray());
            int dayTime = GameEntry.PlayerData.GetData(EnumIntData.DayState);
            Type type;
            if (m_DayStateBaseDic.ContainsKey(dayTime))
            {
                type = m_DayStateBaseDic[dayTime].GetType();
            }
            else
            {
                throw new GameFrameworkException($"This dayState:{dayTime} is no exist,why?");
            }
            StartDayState(type);
            GameEntry.Event.Subscribe(SleepEventArgs.EventId,OnSleep);
        }
        private void OnSleep(object sender,GameEventArgs e)
        {
            //TODO:
            GameEntry.PlayerData.ChangeData(EnumIntData.Day, 1);
        }
        public void ShutdownDayTimer()
        {
            GameEntry.Fsm.DestroyFsm<DayTimer>(m_DayStateFsm);
            GameEntry.Event.Unsubscribe(SleepEventArgs.EventId, OnSleep);
            m_DayStateFsm = null;
        }
        /// <summary>
        /// 初始化时间管理器。
        /// </summary>
        public DayTimer()
        {
            m_DayStateBaseDic = new Dictionary<int, DayStateBase>();
            RegisterDayState<DayMorning>();
            RegisterDayState<DayFishing>();
            RegisterDayState<DayDusk>();
        }
        private void RegisterDayState<T>() where T : DayStateBase, new()
        {
            T dayState = new T();
            m_DayStateBaseDic.Add(dayState.dayStateID, dayState);
        }

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <typeparam name="T">要开始的流程类型。</typeparam>
        private void StartDayState<T>() where T : DayStateBase
        {
            if (m_DayStateFsm == null)
            {
                throw new GameFrameworkException("You must initialize dayState first.");
            }
            m_DayStateFsm.Start<T>();
        }

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <param name="dayStateType">要开始的流程类型。</param>
        private void StartDayState(Type dayStateType)
        {
            if (m_DayStateFsm == null)
            {
                throw new GameFrameworkException("You must initialize dayState first.");
            }
            m_DayStateFsm.Start(dayStateType);
        }
    }
}
