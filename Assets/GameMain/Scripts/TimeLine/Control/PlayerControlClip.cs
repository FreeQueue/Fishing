using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Fishing
{
    [Serializable]
    public class PlayerControlClip : PlayableAsset, ITimelineClipAsset
    {
        public PlayerControlBehaviour template = new PlayerControlBehaviour();
        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<PlayerControlBehaviour>.Create(graph, template);
            return playable;
        }
    }
}