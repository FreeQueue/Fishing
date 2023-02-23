using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Fishing
{
    [TrackColor(0.855f, 0.903f, 0.87f)]
    [TrackClipType(typeof(DialogClip))]
    public class DialogTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var clip in GetClips())
            {
                var customClip = clip.asset as DialogClip;
                if (customClip != null)
                {
                    customClip.m_CustomClipEnd = clip.end;
                }
            }
            return base.CreateTrackMixer(graph, go, inputCount);
        }
    }
}
