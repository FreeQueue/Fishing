using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
namespace Fishing
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/ItemGrid")]
    public class ItemGridGroupComponent : GameFrameworkComponent
    {
        [SerializeField]
        private ItemImage m_ItemImage;
        public ItemImage ItemImage
        {
            get => m_ItemImage;
        }
        [SerializeField]
        private ItemImage m_GridImage;
        public ItemImage GridImage
        {
            get=> m_GridImage;
        }
        [SerializeField]
        private LevelColor m_LevelColor;
        public LevelColor LevelColor
        {
            get=> m_LevelColor;
        }
        Dictionary<EnumGrid, ItemGridGroupHelper> m_itemGridGroupHelperDic = new Dictionary<EnumGrid, ItemGridGroupHelper>();
        public void Init()
        {
            foreach (var itemGridGroupHelper in m_itemGridGroupHelperDic)
            {
                itemGridGroupHelper.Value.Load();
            }
        }
        protected override void Awake()
        {
            base.Awake();
            foreach (var itemGridGroupHelper in GetComponents<ItemGridGroupHelper>())
            {
                itemGridGroupHelper.Init();
                m_itemGridGroupHelperDic.Add((itemGridGroupHelper.EnumType), itemGridGroupHelper);
            }
            Init();
        }
        public void Save()
        {
            foreach (var itemGridGroupHelper in m_itemGridGroupHelperDic)
            {
                itemGridGroupHelper.Value.Save();
            }
        }
        public void Clear()
        {
            foreach (var itemGridGroupHelper in m_itemGridGroupHelperDic)
            {
                itemGridGroupHelper.Value.m_ItemGridGroupBase.RemoveAllItem();
            }
        }
        public ItemGridGroupHelper GetItemGridGroupHelper(EnumGrid enumGrid)
        {
            ItemGridGroupHelper temp;
            if (m_itemGridGroupHelperDic.TryGetValue(enumGrid, out temp))
            {
                return temp;
            }
            return null;
        }
        public ItemGridGroupHelper[] GetAllItemGridGroupHelper()
        {
            List<ItemGridGroupHelper> itemGridGroupHelpers = new List<ItemGridGroupHelper>();
            foreach (var itemGridGroupHelper in m_itemGridGroupHelperDic)
            {
                itemGridGroupHelpers.Add(itemGridGroupHelper.Value);
            }
            return itemGridGroupHelpers.ToArray();
        }
        public void PushItem(int itemID, EnumGrid fromID, EnumGrid toID)
        {
            ItemGridGroupBase fromGroup = GetItemGridGroupHelper(fromID).m_ItemGridGroupBase;
            ItemGridGroupBase toGroup = GetItemGridGroupHelper(toID).m_ItemGridGroupBase;
            int toGridID = toGroup.GetFirstEmptyGrid();
            if (toGridID != -1)
            {
                int fromGridID = fromGroup.GetGridID(itemID);
                if (fromGridID != -1)
                {
                    fromGroup.RemoveItemFromGrid(fromGridID);
                    toGroup.AddItem(itemID);
                }
                else
                {
                    GameEntry.Event.Fire(this, GridGroupHasNotItemEventArgs.Create(itemID, fromID));
                }

            }
            else
            {
                GameEntry.Event.Fire(this, GridGroupHasFullEventArgs.Create(toID));
            }
        }
        public void PushItemByGrid(int gridID, EnumGrid fromID, EnumGrid toID)
        {
            ItemGridGroupBase fromGroup = GetItemGridGroupHelper(fromID).m_ItemGridGroupBase;
            ItemGridGroupBase toGroup = GetItemGridGroupHelper(toID).m_ItemGridGroupBase;
            int toGridID = toGroup.GetFirstEmptyGrid();
            if (toGridID != -1)
            {
                int itemID = fromGroup.GetItemFromGrid(gridID);
                fromGroup.RemoveItemFromGrid(gridID);
                toGroup.AddItem(itemID);
            }
            else
            {
                GameEntry.Event.Fire(this, GridGroupHasFullEventArgs.Create(toID));
            }
        }
    }
}