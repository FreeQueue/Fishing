using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Fishing
{
    public class AddItemEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(AddItemEventArgs).GetHashCode();
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

        public static AddItemEventArgs Create(int itemId,int gridID,EnumGrid gridGroupType, object userData = null)
        {
            AddItemEventArgs addItemEventArgs = ReferencePool.Acquire<AddItemEventArgs>();
            addItemEventArgs.itemID = itemId;
            addItemEventArgs.gridID = gridID;
            addItemEventArgs.gridGroupType = gridGroupType;
            addItemEventArgs.UserData = userData;
            return addItemEventArgs;
        }

        public override void Clear()
        {
            itemID = -1;
            gridID = -1;
            gridGroupType = EnumGrid.None;
        }
    }

}

