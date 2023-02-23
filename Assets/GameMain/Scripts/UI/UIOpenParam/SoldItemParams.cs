using GameFramework;
using Fishing.Data;
using System;
namespace Fishing
{
    public class SoldItemParams : IReference
    {
        public GridItem GridItem
        {
            get;
            private set;
        }
        public Action ConfirmCallback{
            get;
            private set;
        }
        public static SoldItemParams Create(GridItem gridItem,Action confirmCallback)
        {
            SoldItemParams soldItemParams = ReferencePool.Acquire<SoldItemParams>();
            soldItemParams.GridItem = gridItem;
            soldItemParams.ConfirmCallback = confirmCallback;
            return soldItemParams;
        }
        public void Clear()
        {
            GridItem = null;
            ConfirmCallback = null;
        }
    }
}
