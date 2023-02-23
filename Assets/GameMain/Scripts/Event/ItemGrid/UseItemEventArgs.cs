using UnityEngine;
using GameFramework.Event;
using GameFramework;
namespace Fishing
{
    public class UseItemEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(UseItemEventArgs).GetHashCode();
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
        public static UseItemEventArgs Create(object userData = null)
        {
            UseItemEventArgs useItemEventArgs = ReferencePool.Acquire<UseItemEventArgs>();
            useItemEventArgs.UserData = userData;
            return useItemEventArgs;
        }
        public override void Clear()
        {
            UserData = null;
        }
    }
}