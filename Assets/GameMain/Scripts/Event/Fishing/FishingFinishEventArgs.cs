using UnityEngine;
using GameFramework;
using GameFramework.Event;
namespace Fishing
{
    public class FishingFinishEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(FishingFinishEventArgs).GetHashCode();
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
        public static FishingFinishEventArgs Create(object userData = null)
        {
            FishingFinishEventArgs fishingFinishEventArgs = ReferencePool.Acquire<FishingFinishEventArgs>();
            fishingFinishEventArgs.UserData = userData;
            return fishingFinishEventArgs;
        }

        public override void Clear()
        {
        }
    }
}