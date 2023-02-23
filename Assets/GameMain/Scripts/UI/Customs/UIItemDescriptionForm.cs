using UnityEngine.UI;
using UnityEngine;
namespace Fishing
{
    public class UIItemDescriptionForm : UGuiFormEx
    {
        public Text ItemName, ItemType, ItemDescription;
        public Image image;
        public Button closeButton;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Register(InputSys.EnumInput.Cancel, OnCloseButtonClick);
            GridItem gridItem = (GridItem)userData;
            ItemName.text = gridItem.ItemName;
            ItemType.text = gridItem.ItemType;
            ItemDescription.text = gridItem.ItemDescription;
            image.sprite = gridItem.Image.sprite;
        }
        public void OnCloseButtonClick()
        {
            Close();
        }
    }
}
