using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace Fishing
{
    [CreateAssetMenu(fileName = "New TimeLineList", menuName = "Scriptable/Create TimeLineList", order = 3)]
    public class TimeLineList : ScriptableObject
    {
        [SerializeField]
        private List<TimelineAsset> timelineAssets;
        public TimelineAsset GetTimeLineAsset(EnumTimeLine enumTimeLine)
        {
            return timelineAssets[((int)enumTimeLine)];
        }
    }
}