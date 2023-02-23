using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
namespace Fishing
{
    public class StoreItemGrid : ItemGrid
    {
        [SerializeField]
        private Text Price;
        [SerializeField]
        private Transform PriceRoot;
        protected override Action OnClick
        {
            get
            {
                return () =>
                {
                    if(IsEmpty)return;
                    GameEntry.UI.OpenUIForm(EnumUIForm.UIItemSoldForm, SoldItemParams.Create(GridItem, OnSoldCallback));
                };
            }
        }
        protected override Action OnNormal
        {
            get
            {
                return base.OnEmpty +(() =>
                {
                    PriceRoot.gameObject.SetActive(true);
                    Price.text = GridItem.Price.ToString();
                });
            }
        }
        protected override Action OnEmpty
        {
            get
            {
                return base.OnEmpty + (() =>
                {
                    PriceRoot.gameObject.SetActive(false);
                });
            }
        }
        private void OnSoldCallback()
        {
            if (GameEntry.PlayerData.GetData(EnumIntData.Money) >= GridItem.Price)
            {
                GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Prop).m_ItemGridGroupBase.AddItem(GridItem.ID);
                GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Store).m_ItemGridGroupBase.RemoveItemFromGrid(GridID);
                GameEntry.PlayerData.ChangeData(EnumIntData.Money, -GridItem.Price);
            }
            else
            {
                GameEntry.UI.OpenTipsPopForm("你没有足够的钱");
            }

        }
    }
}
