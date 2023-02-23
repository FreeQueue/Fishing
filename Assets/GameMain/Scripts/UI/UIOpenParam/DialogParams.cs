using GameFramework;
using UnityEngine.Events;
using System;
namespace Fishing
{
    public class DialogParams : IReference
    {
        public string Dialog
        {
            get;
            private set;
        }
        public bool IsLeft
        {
            get;
            private set;
        }
        public EnumCharacter Character
        {
            get;
            private set;
        }
        public int VoiceID
        {
            get;
            private set;
        }
        public Action UnitFinishCallback{
            get;
            private set;
        }
        public static DialogParams Create(EnumCharacter character,bool isLeft,string dialog,int voiceID,Action unitFinishCallback=null)
        {
            DialogParams dialogParams = ReferencePool.Acquire<DialogParams>();
            dialogParams.Dialog = dialog;
            dialogParams.IsLeft = isLeft;
            dialogParams.Character = character;
            dialogParams.UnitFinishCallback = unitFinishCallback;
            dialogParams.VoiceID = voiceID;
            return dialogParams;
        }
        public void Clear()
        {
            Dialog = null;
            IsLeft = true;
            Character = EnumCharacter.Player;
            UnitFinishCallback = null;
        }
    }
}
