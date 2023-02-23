using UnityEngine;
using System;
using UnityEngine.Timeline;
using Fishing.Data;
namespace Fishing
{
    public class Trader : NPCBase
    {
        protected override string InteractText
        {
            get
            {
                return "要看看商店吗？";
            }
        }
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            RegisterInteract(Show);
        }


        private void Show()
        {
            if(GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Store).m_ItemGridGroupBase.ItemCount<=0){
                GameEntry.Dialog.StartDialogGroup(303);
                return;
            }
            GameEntry.Dialog.StartDialogGroup(301,OpenStore);
        }
        private void OpenStore()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UIStoreForm);
        }
    }
}