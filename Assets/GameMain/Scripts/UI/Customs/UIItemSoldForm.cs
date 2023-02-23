using UnityEngine.UI;
using UnityEngine;
using System;
namespace Fishing
{
    public class UIItemSoldForm : UGuiFormEx
    {
        [SerializeField]
        private Text ItemName, ItemType, ItemDescription,ItemPrice;
        [SerializeField]
        private Image image;
        [SerializeField]
        private Button confirmButton,cancelButton;
        private Action confirmCallback;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            confirmButton.onClick.AddListener(OnConfirmButtonClick);
            cancelButton.onClick.AddListener(OnCancelButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Register(InputSys.EnumInput.Cancel, OnCancelButtonClick);
            SoldItemParams soldItemParams = userData as SoldItemParams;
            GridItem gridItem = soldItemParams.GridItem;
            confirmCallback = soldItemParams.ConfirmCallback;
            ItemName.text = gridItem.ItemName;
            ItemType.text = gridItem.ItemType;
            ItemPrice.text = gridItem.Price.ToString();
            ItemDescription.text = gridItem.ItemDescription;
            image.sprite = gridItem.Image.sprite;
        }
        private void OnConfirmButtonClick()
        {
            confirmCallback.Invoke();
            Close();
        }
        private void OnCancelButtonClick()
        {
            Close();
        }

    }
}
