using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Fishing
{
    public class PlayableBehaviourEx : PlayableBehaviour
    {
        public enum PlayableState
        {
            Init,
            Start,
            Update
        }
        public PlayableState playableState
        {
            get;
            private set;
        } = PlayableState.Init;

        public virtual void Init(Playable playable, FrameData info)
        {

        }

        public virtual void Start(Playable playable, FrameData info)
        {

        }

        public virtual void Update(Playable playable, FrameData info)
        {

        }

        public virtual void Pause(Playable playable, FrameData info)
        {

        }
        // public virtual void Leave(Playable playable, FrameData info)
        // {

        // }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (playableState == PlayableState.Init)//Init
            {
                Init(playable, info);
                playableState = PlayableState.Start;
            }
            else if (playableState == PlayableState.Update||playableState==PlayableState.Start)
            {
                Pause(playable, info);
            }
        }
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            if (playableState == PlayableState.Start)
            {
                Start(playable, info);
                playableState = PlayableState.Update;
            }
            else if (playableState == PlayableState.Update)
            {
                Update(playable, info);
            }
        }
    }
}