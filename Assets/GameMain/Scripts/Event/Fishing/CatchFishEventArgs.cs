using UnityEngine;
using GameFramework;
using GameFramework.Event;
namespace Fishing
{
    public class CatchFishEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(CatchFishEventArgs).GetHashCode();
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
        public static CatchFishEventArgs Create(object userData = null)
        {
            CatchFishEventArgs catchFishEventArgs = ReferencePool.Acquire<CatchFishEventArgs>();
            catchFishEventArgs.UserData = userData;
            return catchFishEventArgs;
        }

        public override void Clear()
        {
        }
    }
}