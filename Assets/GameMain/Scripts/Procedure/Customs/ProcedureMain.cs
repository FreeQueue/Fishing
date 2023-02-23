using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Fishing
{
    public class ProcedureMain : ProcedureBase
    {
        public MainController m_MainController;
        private ProcedureOwner procedureOwner;
        private bool changeScene = false;
        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            // GameEntry.Sound.PlayMusic(EnumSound.背景);
            this.procedureOwner = procedureOwner;
            this.changeScene = false;
            GameEntry.Event.Subscribe(ChangeSceneEventArgs.EventId, OnChangeScene);
            StartMainGame();
        }
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (changeScene)
            {
                ChangeState<ProcedureLoadingScene>(procedureOwner);
            }
        }
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(ChangeSceneEventArgs.EventId, OnChangeScene);
            ShutdownMainGame();
        }
        private void OnChangeScene(object sender, GameEventArgs e)
        {
            ChangeSceneEventArgs ne = (ChangeSceneEventArgs)e;
            if (ne == null)
                return;
            changeScene = true;
            procedureOwner.SetData<VarInt32>(Constant.ProcedureData.NextSceneId, ne.SceneId);
        }
        public void StartMainGame()
        {
            if (m_MainController == null)
            {
                m_MainController = MainController.Create();
            }
            m_MainController.Start();
        }
        public void ShutdownMainGame()
        {
            m_MainController.Shutdown();
            GameEntry.ItemGrid.Save();
        }
    }
}