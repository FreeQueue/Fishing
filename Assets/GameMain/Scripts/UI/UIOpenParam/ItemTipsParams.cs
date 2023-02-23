using GameFramework;
namespace Fishing
{
    public class ItemTipsParams : IReference
    {
        public string ItemName
        {
            get;
            private set;
        }
        public static ItemTipsParams Create(GridItem gridItem)
        {
            ItemTipsParams itemTipsParams = ReferencePool.Acquire<ItemTipsParams>();
            itemTipsParams.ItemName = gridItem.ItemName;
            return itemTipsParams;
        }
        public void Clear()
        {
            ItemName = null;
        }
    }
}
