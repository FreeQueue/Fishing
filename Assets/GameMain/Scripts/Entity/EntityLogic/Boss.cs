using UnityEngine;
using GameFramework.Fsm;
using UnityEngine.Timeline;
using System;
namespace Fishing
{
    public class Boss : NPCBase
    {
        private string m_InteractText;
        protected override string InteractText
        {
            get
            {
                return "按'F'对话";
            }
        }
        protected override void OnMorning()
        {
            base.OnMorning();
            RegisterInteract(Morning);
        }
        protected override void OnFishing()
        {
            base.OnFishing();
            RegisterInteract(Fishing);
            //TODO:
        }
        protected override void OnDusk()
        {
            base.OnDusk();
            RegisterInteract(Dusk);
        }
        private void Morning()
        {
            if (GameEntry.PlayerData.IsNeedUpgrade)
            {
                GameEntry.Dialog.StartDialogGroup(203,GiveNewFishingRod);
              
            }
            else
            {
                GameEntry.Dialog.StartDialogGroup(204,OpenAnalysis);
            }
        }
        private void Fishing()
        {
            GameEntry.Dialog.StartDialogGroup(205);
        }
        private void Dusk()
        {
            if (GameEntry.PlayerData.IsNeedSubmit)
            {
                GameEntry.Dialog.StartDialogGroup(201,Submit);
            }
            else
            {
                GameEntry.Dialog.StartDialogGroup(204,OpenAnalysis);
            }
        }
        private void Submit()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UISubmitForm);
        }
        private void GiveNewFishingRod()
        {
            GameEntry.PlayerData.ChangeData(EnumIntData.RodLevel, 1);
        }
        private void OpenAnalysis()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UIAnalysisForm);
        }
    }
}