using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Fishing.Data;
using GameFramework.Event;
using System;
namespace Fishing
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Dialog")]
    public class DialogComponent : GameFrameworkComponent
    {
        private IEnumerator enumerator;
        private List<DialogUnitData> dialogUnitDatas;
        private Action m_DialogGroupFinishCallback;
        private bool IsLast;
        public void StartDialogGroup(int dialogGroupID, Action dialogGroupFinishCallback = null)
        {
            m_DialogGroupFinishCallback = dialogGroupFinishCallback;
            dialogUnitDatas = GameEntry.Data.GetData<DataDialogGroup>().GetDialogGroupData(dialogGroupID);
            if (dialogUnitDatas == null)
            {
                Log.Error("Has no dialogGroup's ID:" + dialogGroupID);
                return;
            }
            enumerator = dialogUnitDatas.GetEnumerator();
            enumerator.MoveNext();
            GameEntry.Event.Subscribe(DialogUnitFinishEventArgs.EventId, DialogFinishCallback);
            PushDialog();
        }
        private void PushDialog()
        {
            DialogUnitData dialogUnitData = enumerator.Current as DialogUnitData;
            GameEntry.UI.OpenUIForm(EnumUIForm.UIDialogForm, DialogParams.Create((EnumCharacter)dialogUnitData.CharacterID, dialogUnitData.IsLeft, dialogUnitData.Dialog,dialogUnitData.VoiceID));
            if (!enumerator.MoveNext())
            {
                dialogUnitDatas = null;
                enumerator = null;
                IsLast = true;
            }
        }
        private void DialogFinishCallback(object sender, GameEventArgs e)
        {
            if (IsLast)
            {
                if (m_DialogGroupFinishCallback != null)
                {
                    m_DialogGroupFinishCallback.Invoke();
                }
                GameEntry.Event.Unsubscribe(DialogUnitFinishEventArgs.EventId, DialogFinishCallback);
                IsLast = false;
            }
            else
            {
                PushDialog();
            }
        }
    }
}