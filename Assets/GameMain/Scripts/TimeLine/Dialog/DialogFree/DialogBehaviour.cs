using System;
using UnityEngine.Playables;
using UnityEngine;
namespace Fishing
{
    [Serializable]
    public class DialogBehaviour : PlayableBehaviourEx
    {
        public EnumCharacter enumCharacter;
        public bool isLeft;
        public string dialog;
        public bool hasToPause;
        private bool isPlayed = false;
        private int? dialogUISerialID;
        [HideInInspector]
        public double endTime;
        private Playable director;
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (!isPlayed)
            {
                dialogUISerialID = GameEntry.UI.OpenUIForm(EnumUIForm.UIDialogForm, DialogParams.Create(enumCharacter, isLeft, dialog,0,OnDialogEnd));
                isPlayed = true;
            }
        }
        public override void Start(Playable playable, FrameData info)
        {
            director = playable;
        }
        public override void Pause(Playable playable, FrameData info)
        {
            if (hasToPause)
            {
                hasToPause = false;
                GameEntry.TimeLine.PauseTimeLine();
            }
            else if (dialogUISerialID != null)
            {
                GameEntry.UI.CloseUIForm((int)dialogUISerialID);
            }
        }
        public override void OnGraphStart(Playable playable)
        {
            isPlayed = false;
        }
        private void OnDialogEnd()
        {
            (director.GetGraph().GetResolver() as PlayableDirector).time = endTime;
            hasToPause = false;
            dialogUISerialID = null;
            GameEntry.TimeLine.ResumeTimeLine();
        }
    }
}
