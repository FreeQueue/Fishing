using UnityEngine;
using GameFramework.Event;
using GameFramework;
namespace Fishing
{
    public class MainGameStartEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(MainGameStartEventArgs).GetHashCode();
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
        public static MainGameStartEventArgs Create(object userData = null)
        {
            MainGameStartEventArgs mainGameStartEventArgs = ReferencePool.Acquire<MainGameStartEventArgs>();
            return mainGameStartEventArgs;
        }
        public override void Clear()
        {
        }
    }
}