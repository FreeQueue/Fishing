using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using GameFramework;

namespace Fishing
{
    public class ClickItemEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ClickItemEventArgs).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public GridItem GridItem
        {
            get;
            private set;
        }
        public object UserData
        {
            get;
            private set;
        }
        public static ClickItemEventArgs Create(GridItem gridItem, object userData = null)
        {
            ClickItemEventArgs clickItemEventArgs = ReferencePool.Acquire<ClickItemEventArgs>();
            clickItemEventArgs.GridItem = gridItem;
            clickItemEventArgs.UserData = userData;
            return clickItemEventArgs;
        }
        public override void Clear()
        {
            GridItem = null;
        }
    }
}