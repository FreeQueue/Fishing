using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Fishing
{
    [TrackColor(0.855f, 0.903f, 0.87f)]
    [TrackClipType(typeof(PlayerControlClip))]
    public class PlayerControlTrack : TrackAsset
    {
        [SerializeField]
        private EnumPlayerControlTrackModel PlayerControlTrackModel;
        public enum EnumPlayerControlTrackModel
        {
            Enable,
            Disable,
        }
        protected override void OnCreateClip(TimelineClip clip)
        {
            clip.displayName = "Control";
            base.OnCreateClip(clip);
        }
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixer = PlayerControlMixerPlayable.Create(graph, inputCount);
            mixer.GetBehaviour().ControlTrackModel=PlayerControlTrackModel;
            return mixer;
        }
    }
}
