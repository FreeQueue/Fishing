using Fishing.Data;
using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class GridItem : ItemLogicEx
    {
        public int ID { get => gridItemData.ID; }
        public string ItemName { get => gridItemData.ItemName; }
        public string ItemType { get => gridItemData.ItemType; }
        public int Level { get => gridItemData.Level; }
        public string ItemDescription { get => gridItemData.ItemDescription; }
        public int Price { get => gridItemData.Price; }
        [SerializeField]
        private Image m_Image;
        public Image Image { get => m_Image; }
        private IGridItemData gridItemData;
        public void SetItemData(int itemID)
        {
            gridItemData = GridItemDataMaker.GetGridItemData(itemID);
            Image.sprite = GameEntry.ItemGrid.ItemImage.GetImage(gridItemData.ImageID);
        }
    }
}
