using UnityEngine;
using System.Collections.Generic;
using GameFramework.Event;
namespace Fishing
{
    public class GridController
    {
        private UGuiFormEx uGuiFormEx;
        private EnumGrid gridGroupType;
        private ItemGrid[] itemGrids;
        private Transform gridRoot;
        private ItemGridGroupHelper itemGridGroupHelper;
        public ItemGridGroupBase GroupBase
        {
            get
            {
                return itemGridGroupHelper.m_ItemGridGroupBase;
            }
        }
        public ItemGridGroupHelper Group
        {
            get
            {
                return itemGridGroupHelper;
            }
        }
        public GridController(UGuiFormEx uGuiFormEx, EnumGrid enumGrid, Transform gridRoot)
        {
            this.uGuiFormEx = uGuiFormEx;
            this.gridGroupType = enumGrid;
            this.gridRoot = gridRoot;
            this.itemGridGroupHelper = GameEntry.ItemGrid.GetItemGridGroupHelper(enumGrid);
            itemGrids = new ItemGrid[Group.ItemGridCount];
        }
        public ItemGrid[] GetItemGrids()
        {
            return itemGrids;
        }
        public void ShowItemGrids()
        {
            int j = 0;
            for (int i = 0; i < Group.ItemGridCount; i++)
            {
                uGuiFormEx.ShowItem<ItemGrid>(EnumItem.ItemGrid, item =>
                {
                    item.transform.SetParent(gridRoot, false);
                    ItemGrid itemGrid = item.Logic as ItemGrid;
                    itemGrids[j] = itemGrid;
                    itemGrid.SetItemGrid(gridGroupType, j);
                    int itemID = GroupBase.GetItemFromGrid(j);
                    if (itemID != -1)
                    {
                        itemGrid.AddItem(uGuiFormEx, itemID);
                    }
                    j++;
                });
            }
        }
        public void OnAddItem(object sender, GameEventArgs e)
        {
            AddItemEventArgs ne = (AddItemEventArgs)e;
            if (ne == null) return;
            if (ne.gridGroupType != gridGroupType) return;
            itemGrids[ne.gridID].AddItem(uGuiFormEx, ne.itemID);
        }
        public void OnRemoveItem(object sender, GameEventArgs e)
        {
            RemoveItemEventArgs ne = (RemoveItemEventArgs)e;
            if (ne == null) return;
            if (ne.gridGroupType != gridGroupType) return;
            itemGrids[ne.gridID].RemoveItem(uGuiFormEx);
        }
    }
}