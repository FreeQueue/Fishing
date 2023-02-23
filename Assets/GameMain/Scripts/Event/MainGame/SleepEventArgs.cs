using UnityEngine;
using GameFramework.Event;
using GameFramework;
namespace Fishing
{
    public class SleepEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(SleepEventArgs).GetHashCode();
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
        public static SleepEventArgs Create(object userData = null)
        {
            SleepEventArgs sleepEventArgs = ReferencePool.Acquire<SleepEventArgs>();
            return sleepEventArgs;
        }
        public override void Clear()
        {
        }
    }
}