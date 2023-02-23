using UnityEngine;
using GameFramework.Event;
using GameFramework;
namespace Fishing
{
    public class AddBuffEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(AddBuffEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public int BuffIndex{
            get;
            private set;
        }
        public int Level{
            get;
            private set;
        }
        public object UserData
        {
            get;
            private set;
        }
        public static AddBuffEventArgs Create(int buffIndex,int level,object userData = null)
        {
            AddBuffEventArgs addBuffEventArgs = ReferencePool.Acquire<AddBuffEventArgs>();
            addBuffEventArgs.BuffIndex = buffIndex;
            addBuffEventArgs.Level = level;
            addBuffEventArgs.UserData = userData;
            return addBuffEventArgs;
        }
        public override void Clear()
        {
            UserData = null;
        }
    }
}