using UnityEngine;
using UnityEngine.UI;
using Fishing.Data;
using System;
using System.Collections;
namespace Fishing
{

    public class UISubmitForm : UGuiFormEx
    {
        [SerializeField]
        private Button m_ConfirmButton;
        [SerializeField]
        private RectTransform m_SubmitGridRoot;
        private GridController m_GridController;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_GridController = new GridController(this, EnumGrid.Bag, m_SubmitGridRoot);
            m_ConfirmButton.onClick.AddListener(OnSubmit);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            //GameEntry.PlayerData.IsNeedSubmit
            Subscribe(AddItemEventArgs.EventId, m_GridController.OnAddItem);
            Subscribe(RemoveItemEventArgs.EventId, m_GridController.OnRemoveItem);
            m_GridController.ShowItemGrids();
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            GameEntry.Dialog.StartDialogGroup(202);
        }
        private void OnSubmit()
        {
            StartCoroutine("Submit");

        }
        private IEnumerator Submit(){
            foreach (var item in m_GridController.GetItemGrids())
            {
                if (item.IsEmpty) continue;
                switch (Enum.Parse(typeof(EnumItemType), item.GridItem.ItemType))
                {
                    case EnumItemType.Mineral:
                        GameEntry.PlayerData.ChangeData(EnumIntData.Money, item.GridItem.Price);
                        break;
                    case EnumItemType.Fortify:
                        GameEntry.PlayerData.ChangeData(EnumIntData.FortifyNum, 1);
                        break;
                    case EnumItemType.Relic:
                        GameEntry.PlayerData.ChangeData(EnumIntData.RelicNum, 1);
                        break;
                }
                yield return new WaitForSeconds(0.3f);
                GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.RemoveItemFromGrid(item.GridID);
            }
            Close();
        }
    }
}