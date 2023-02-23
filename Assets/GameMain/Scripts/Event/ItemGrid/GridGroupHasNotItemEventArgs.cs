using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Fishing
{
    public class GridGroupHasNotItemEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GridGroupHasNotItemEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int itemID
        {
            get;
            private set;
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

        public static GridGroupHasNotItemEventArgs Create(int itemId,EnumGrid gridGroupID, object userData = null)
        {
            GridGroupHasNotItemEventArgs gridGroupHasNotItemEventArgs = ReferencePool.Acquire<GridGroupHasNotItemEventArgs>();
            gridGroupHasNotItemEventArgs.itemID = itemId;
            gridGroupHasNotItemEventArgs.gridGroupID = gridGroupID;
            gridGroupHasNotItemEventArgs.UserData = userData;
            return gridGroupHasNotItemEventArgs;
        }

        public override void Clear()
        {
            itemID = -1;
            gridGroupID = EnumGrid.None;
        }
    }

}

