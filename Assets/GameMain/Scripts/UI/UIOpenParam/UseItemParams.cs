using GameFramework;
using Fishing.Data;
using System;
namespace Fishing
{
    public class UseItemParams : IReference
    {
        public GridItem GridItem
        {
            get;
            private set;
        }
        public Action UseCallback{
            get;
            private set;
        }
        public static UseItemParams Create(GridItem gridItem,Action useCallback)
        {
            UseItemParams useItemParams = ReferencePool.Acquire<UseItemParams>();
            useItemParams.GridItem = gridItem;
            useItemParams.UseCallback = useCallback;
            return useItemParams;
        }
        public void Clear()
        {
            GridItem = null;
            UseCallback = null;
        }
    }
}
