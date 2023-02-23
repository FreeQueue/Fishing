using UnityEngine;
using GameFramework.Fsm;
using GameFramework;
using System.Collections.Generic;
using System;
using Fishing.InputSys;
namespace Fishing
{
    [RequireComponent(typeof(PlayerTrigger))]
    public abstract class NPCBase : EntityLogicEx
    {
        private PlayerTrigger m_PlayerTrigger;
        private int? m_InteractUI;
        private Action m_InteractAction;
        protected virtual string InteractText
        {
            get;
        } = "交互";
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            EntityData entityData = userData as EntityData;

            m_PlayerTrigger = GetComponent<PlayerTrigger>();
            m_PlayerTrigger.Register(OnEnterTrigger, OnExitTrigger);
            Subscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.DayState), OnDayStateChange);
        }
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            EntityData entityData = userData as EntityData;
            transform.position = entityData.Position;
            transform.rotation = entityData.Rotation;
            m_PlayerTrigger.SetActive(true);
            SetState(GameEntry.PlayerData.GetData(EnumIntData.DayState));
        }
        /// <summary>
        /// 隐藏实体，注意所有事件将取消订阅
        /// </summary>
        /// <param name="isShutdown"></param>
        /// <param name="userData"></param>
        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            m_PlayerTrigger.SetActive(false);
            UnSubscribeAll();
        }
        private void SetState(int state)
        {
            switch (state)
            {
                case 0:
                    OnMorning();
                    break;
                case 1:
                    OnFishing();
                    break;
                case 2:
                    OnDusk();
                    break;
            }
        }
        private void OnDayStateChange(object sender, GameFrameworkEventArgs e)
        {
            PlayerDataChangeEventArgs ne = e as PlayerDataChangeEventArgs;
            SetState((int)ne.Data);
            RefreshUI();
        }
        protected virtual void OnEnterTrigger()
        {
            m_InteractUI = GameEntry.UI.OpenUIForm(EnumUIForm.UIInteractForm, InteractParams.Create(InteractText));
            Register(EnumInput.Interact, m_InteractAction);
        }
        protected virtual void OnExitTrigger()
        {
            if (m_InteractUI != null)
            {
                GameEntry.UI.CloseUIForm(((int)m_InteractUI));
                m_InteractUI = null;
            }
            Unregister(EnumInput.Interact);
        }
        private void RefreshUI()
        {
            if (m_InteractUI != null)
            {
                GameEntry.UI.CloseUIForm(((int)m_InteractUI));
                m_InteractUI = GameEntry.UI.OpenUIForm(EnumUIForm.UIInteractForm, InteractParams.Create(InteractText));
            }
        }
        protected virtual void OnMorning()
        {

        }
        protected virtual void OnFishing()
        {

        }
        protected virtual void OnDusk()
        {

        }
        protected void RegisterInteract(Action action)
        {
            m_InteractAction = action;
        }
        protected void UnregisterInteract()
        {
            m_InteractAction = null;
        }
    }
}