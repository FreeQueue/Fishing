using UnityEngine;
using UnityEngine.Playables;
using static Fishing.PlayerControlTrack;
namespace Fishing
{
    public class PlayerControlMixerPlayable : PlayableBehaviour
    {
        public PlayerControlTrack.EnumPlayerControlTrackModel ControlTrackModel{get; set;}
        private PlayerControlBehaviour activeBehavior;
        public static ScriptPlayable<PlayerControlMixerPlayable> Create(PlayableGraph graph, int inputCount)
        {
            return ScriptPlayable<PlayerControlMixerPlayable>.Create(graph, inputCount);
        }
        private bool isCanControlCache;
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (playable.GetInputWeight(activeBehavior.index) > 0) return;
            GetActive(playable);
            if (activeBehavior != null)
            {
                GameEntry.Input.IsCanControl = ControlTrackModel == EnumPlayerControlTrackModel.Enable;
            }
            else
            {
                GameEntry.Input.IsCanControl = ControlTrackModel == EnumPlayerControlTrackModel.Disable;
            }
        }
        public override void OnPlayableCreate(Playable playable)
        {
            isCanControlCache = GameEntry.Input.IsCanControl;
        }
        public override void OnPlayableDestroy(Playable playable)
        {
            GameEntry.Input.IsCanControl = isCanControlCache;
        }

        private void GetActive(Playable playable)
        {
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                if (playable.GetInputWeight(i) > 0)
                {
                    activeBehavior = playable.GetInput(i).GetGraph().GetResolver() as PlayerControlBehaviour;
                    activeBehavior.index = i;
                    return;
                }
            }
        }
        private void SetControl(bool value)
        {
            GameEntry.Input.IsCanControl = value;
        }
    }
}