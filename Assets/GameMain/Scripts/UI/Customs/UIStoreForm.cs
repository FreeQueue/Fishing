using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class UIStoreForm : UGuiFormEx
    {
        [SerializeField]
        private RectTransform m_StoreGridRoot;
        [SerializeField]
        private Button m_CloseButton;
        [SerializeField]
        private Text m_Money;
        private EnumGrid m_GridGroupType;
        private StoreItemGrid[] m_ItemGrids;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_ItemGrids = new StoreItemGrid[GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Store).ItemGridCount];
            m_CloseButton.onClick.AddListener(OnCloseButtonClick);
            m_GridGroupType = EnumGrid.Store;
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Subscribe(AddItemEventArgs.EventId, OnAddItem);
            Subscribe(RemoveItemEventArgs.EventId, OnRemoveItem);
            Subscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.Money), OnMoneyChange);
            m_Money.text = GameEntry.PlayerData.GetData(EnumIntData.Money).ToString();
            ItemGridGroupBase GroupBase = GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Store).m_ItemGridGroupBase;
            int j = 0;
            foreach (var item in GroupBase.GetAllItem())
            {
                ShowItem<StoreItemGrid>(EnumItem.StoreItemGrid,(item)=>{
                    item.transform.SetParent(m_StoreGridRoot);
                    StoreItemGrid itemGrid = item.Logic as StoreItemGrid;
                    m_ItemGrids[j] = itemGrid;
                    itemGrid.SetItemGrid(m_GridGroupType, j);
                    int itemID = GroupBase.GetItemFromGrid(j);
                    if (itemID != -1)
                    {
                        itemGrid.AddItem(this, itemID);
                    }
                    j++;
                });
            }
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            GameEntry.Dialog.StartDialogGroup(302);
        }
        public void OnAddItem(object sender, GameEventArgs e)
        {
            AddItemEventArgs ne = (AddItemEventArgs)e;
            if (ne == null) return;
            if (ne.gridGroupType != m_GridGroupType) return;
            m_ItemGrids[ne.gridID].AddItem(this, ne.itemID);
        }
        public void OnRemoveItem(object sender, GameEventArgs e)
        {
            RemoveItemEventArgs ne = (RemoveItemEventArgs)e;
            if (ne == null) return;
            if (ne.gridGroupType != m_GridGroupType) return;
            m_ItemGrids[ne.gridID].RemoveItem(this);
        }
        private void OnMoneyChange(object sender, GameEventArgs e)
        {
            PlayerDataChangeEventArgs ne = e as PlayerDataChangeEventArgs;
            m_Money.text = ((int)ne.Data).ToString();
        }
        public void OnCloseButtonClick()
        {
            Close();
        }
    }
}