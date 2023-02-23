using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Fishing
{
    public class GetItemEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(GetItemEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public GridItem item
        {
            get;
            private set;
        }
        public object UserData
        {
            get;
            private set;
        }
        public static GetItemEventArgs Create(GridItem gridItem, object userData = null)
        {
            GetItemEventArgs getItemEventArgs = ReferencePool.Acquire<GetItemEventArgs>();
            getItemEventArgs.item = gridItem;
            getItemEventArgs.UserData = userData;
            return getItemEventArgs;
        }
        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}