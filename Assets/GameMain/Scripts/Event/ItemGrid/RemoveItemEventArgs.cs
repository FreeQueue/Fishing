using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Fishing
{
    public class RemoveItemEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(RemoveItemEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int gridID
        {
            get;
            private set;
        }
        public EnumGrid gridGroupType
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static  RemoveItemEventArgs Create(int gridID,EnumGrid gridGroupID, object userData = null)
        {
            RemoveItemEventArgs removeItemEventArgs = ReferencePool.Acquire< RemoveItemEventArgs>();
            removeItemEventArgs.gridID = gridID;
            removeItemEventArgs.gridGroupType = gridGroupID;
            removeItemEventArgs.UserData = userData;
            return removeItemEventArgs;
        }

        public override void Clear()
        {
            gridID = -1;
            gridGroupType = EnumGrid.None;
        }
    }

}

