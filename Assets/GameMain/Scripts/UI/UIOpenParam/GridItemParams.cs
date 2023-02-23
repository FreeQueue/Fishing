using GameFramework;
using Fishing.Data;
namespace Fishing
{
    public class GridItemParams : IReference
    {
        public GridItem GridItem
        {
            get;
            private set;
        }
        public static GridItemParams Create(GridItem gridItem)
        {
            GridItemParams gridItemParams = ReferencePool.Acquire<GridItemParams>();
            gridItemParams.GridItem = gridItem;
            return gridItemParams;
        }
        public void Clear()
        {
            GridItem = null;
        }
    }
}
