using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Fishing
{
    [TrackColor(0.855f, 0.903f, 0.87f)]
    [TrackClipType(typeof(InputClip))]
    public class InputTrack : TrackAsset
    {
        
        protected override void OnCreateClip(TimelineClip clip)
        {
            clip.displayName = "Input";
            base.OnCreateClip(clip);
        }
    }
}
