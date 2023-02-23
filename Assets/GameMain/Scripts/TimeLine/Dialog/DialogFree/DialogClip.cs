using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Fishing
{
    [Serializable]
    public class DialogClip : PlayableAsset, ITimelineClipAsset
    {
        public DialogBehaviour template = new DialogBehaviour();
        [HideInInspector]
        public double m_CustomClipEnd;
        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DialogBehaviour>.Create(graph, template);
            playable.GetBehaviour().endTime = m_CustomClipEnd;
            return playable;
        }
    }
}