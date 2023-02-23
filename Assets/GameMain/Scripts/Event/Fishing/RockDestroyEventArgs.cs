using UnityEngine;
using GameFramework;
using GameFramework.Event;
namespace Fishing
{
    public class RockDestroyEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(RockDestroyEventArgs).GetHashCode();
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
        public static RockDestroyEventArgs Create(object userData = null)
        {
            RockDestroyEventArgs rockDestroyEventArgs = ReferencePool.Acquire<RockDestroyEventArgs>();
            rockDestroyEventArgs.UserData = userData;
            return rockDestroyEventArgs;
        }

        public override void Clear()
        {
        }
    }
}