using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityGameFramework.Runtime;
using GameFramework;
namespace Fishing
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Timeline")]
    public class TimeLineComponent : GameFrameworkComponent
    {
        [SerializeField]
        private PlayableDirector playableDirector;
        [SerializeField]
        private TimeLineList timeLineList;
        protected override void Awake()
        {
            base.Awake();
            playableDirector.playableAsset = null;
        }
        public void PlayTimeLine(EnumTimeLine enumTimeLine)
        {
            if(playableDirector.playableAsset!=null)
            {
                throw new GameFrameworkException($"Timeline:{playableDirector.playableAsset} is playing");
            }
            playableDirector.Play(timeLineList.GetTimeLineAsset(enumTimeLine));
        }
        public void PlayTimeLine(TimelineAsset timelineAsset)
        {
            if(playableDirector.playableAsset!=null)
            {
                throw new GameFrameworkException($"Timeline:{playableDirector.playableAsset} is playing");
            }
            playableDirector.Play(timelineAsset);
        }
        public void PauseTimeLine()
        {
            if (playableDirector.playableAsset != null)
                playableDirector.Pause();
        }
        public void ResumeTimeLine()
        {
            if (playableDirector.playableAsset != null)
                playableDirector.Resume();
        }
        public void StopTimeLine()
        {
            playableDirector.Stop();
            playableDirector.playableAsset = null;
        }
    }
}
