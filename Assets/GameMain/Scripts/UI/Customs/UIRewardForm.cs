using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fishing.Data;
namespace Fishing
{
    public class UIRewardForm : UGuiFormEx
    {
        [SerializeField]
        private RectTransform m_SpoilGridRoot;
        [SerializeField]
        private Button m_ConfirmButton;
        int[] IDs;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_ConfirmButton.onClick.AddListener(OnCloseButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            GameEntry.Sound.PlayMusic(EnumSound.获得物品);
            IDs = userData as int[];
            int j = 0;
            foreach (var spoilID in IDs)
            {
                ShowItem<ItemGrid>(EnumItem.ItemGrid, (item) =>
                {
                    item.transform.SetParent(m_SpoilGridRoot, false);
                    ItemGrid itemGrid = item.Logic as ItemGrid;
                    itemGrid.AddItem(this, IDs[j++]);
                });
            }
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            IDs = null;
        }
        public void OnCloseButtonClick()
        {
            Close();
        }
    }
}