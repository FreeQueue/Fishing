using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Fishing
{
    [Serializable]
    public class InputClip : PlayableAsset, ITimelineClipAsset
    {
        public InputBehaviour template = new InputBehaviour();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<InputBehaviour>.Create(graph, template);
            return playable;
        }
    }
}