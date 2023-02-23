using UnityEngine;
using GameFramework;
using GameFramework.Event;
namespace Fishing
{
    public class StartFishingEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(StartFishingEventArgs).GetHashCode();
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
        public static StartFishingEventArgs Create(object userData = null)
        {
            StartFishingEventArgs startFishingEventArgs = ReferencePool.Acquire<StartFishingEventArgs>();
            startFishingEventArgs.UserData = userData;
            return startFishingEventArgs;
        }

        public override void Clear()
        {
        }
    }
}