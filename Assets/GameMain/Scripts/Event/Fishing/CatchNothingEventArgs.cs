using UnityEngine;
using GameFramework;
using GameFramework.Event;
namespace Fishing
{
    public class CatchNothingEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(CatchNothingEventArgs).GetHashCode();
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
        public static CatchNothingEventArgs Create(object userData = null)
        {
            CatchNothingEventArgs catchNothingEventArgs = ReferencePool.Acquire<CatchNothingEventArgs>();
            catchNothingEventArgs.UserData = userData;
            return catchNothingEventArgs;
        }

        public override void Clear()
        {
        }
    }
}