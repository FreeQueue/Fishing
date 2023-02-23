using UnityEngine;
using GameFramework.Event;
using GameFramework;
namespace Fishing
{
    public class DialogUnitFinishEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(DialogUnitFinishEventArgs).GetHashCode();
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
        public static DialogUnitFinishEventArgs Create(object userData = null)
        {
            DialogUnitFinishEventArgs dialogUnitFinishEventArgs = ReferencePool.Acquire<DialogUnitFinishEventArgs>();
            dialogUnitFinishEventArgs.UserData = userData;
            return dialogUnitFinishEventArgs;
        }

        public override void Clear()
        {
        }
    }
}