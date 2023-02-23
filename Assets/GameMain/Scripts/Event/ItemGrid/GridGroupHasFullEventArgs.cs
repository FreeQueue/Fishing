using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Fishing
{
    public class GridGroupHasFullEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GridGroupHasFullEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public EnumGrid gridGroupID
        {
            get;
            private set;
        }
        public object UserData
        {
            get;
            private set;
        }

        public static GridGroupHasFullEventArgs Create(EnumGrid gridGroupID, object userData = null)
        {
            GridGroupHasFullEventArgs gridGroupHasFullEventArgs = ReferencePool.Acquire<GridGroupHasFullEventArgs>();
            gridGroupHasFullEventArgs.gridGroupID = gridGroupID;
            gridGroupHasFullEventArgs.UserData = userData;
            return gridGroupHasFullEventArgs;
        }

        public override void Clear()
        {
            gridGroupID = EnumGrid.None;
        }
    }

}

