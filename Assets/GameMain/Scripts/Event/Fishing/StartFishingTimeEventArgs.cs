using UnityEngine;
using GameFramework;
using GameFramework.Event;
namespace Fishing
{
    public class StartFishingTimeEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(StartFishingTimeEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public object UserData
        {
            get;
            private set;
        }
        public static StartFishingTimeEventArgs Create(object userData = null)
        {
            StartFishingTimeEventArgs startFishingTimeEventArgs = ReferencePool.Acquire<StartFishingTimeEventArgs>();
            startFishingTimeEventArgs.UserData = userData;
            return startFishingTimeEventArgs;
        }
        public override void Clear()
        {
        }
    }
}