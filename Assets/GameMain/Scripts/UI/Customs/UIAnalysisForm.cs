using UnityEngine;
using UnityEngine.UI;
using Fishing.Data;
namespace Fishing
{
    public class UIAnalysisForm : UGuiFormEx
    {
        [SerializeField]
        private Button closeButton;
        [SerializeField]
        private RectTransform m_RelicGridRoot, m_Fortify1, m_Fortify2;
        [SerializeField]
        private Text text;
        private ItemGrid m_FortifyGrid1, m_FortifyGrid2;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            closeButton.onClick.AddListener(Close);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            GameEntry.Input.Register(InputSys.EnumInput.Cancel, Close);
            int relicNum = GameEntry.PlayerData.GetData(EnumIntData.RelicNum);
            text.text = GameEntry.PlayerData.GetData(EnumIntData.RodLevel).ToString();
            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                ShowItem<ItemGrid>(EnumItem.ItemGrid, (item) =>
                {
                    item.transform.SetParent(m_RelicGridRoot);
                    ItemGrid itemGrid = item.Logic as ItemGrid;
                    itemGrid.SetItemGrid(EnumGrid.Relic, j);
                    if (j < relicNum)
                    {
                        itemGrid.AddItem(this, GameEntry.Data.GetData<DataSpoil>().GetRelic(j).ID);
                    }
                    j++;
                });

            }
            int fortifyNum = GameEntry.PlayerData.GetData(EnumIntData.FortifyNum);
            int rodLevel = GameEntry.PlayerData.GetData(EnumIntData.RodLevel);
            ShowItem<ItemGrid>(EnumItem.ItemGrid, (item) =>
            {
                item.transform.SetParent(m_Fortify1);
                item.transform.localPosition = Vector3.zero;
                ItemGrid itemGrid = item.Logic as ItemGrid;
                m_FortifyGrid1 = itemGrid;
                itemGrid.SetItemGrid(EnumGrid.Fortify, 0);
            });
            ShowItem<ItemGrid>(EnumItem.ItemGrid, (item) =>
            {
                item.transform.SetParent(m_Fortify2);
                item.transform.localPosition = Vector3.zero;
                ItemGrid itemGrid = item.Logic as ItemGrid;
                m_FortifyGrid2 = itemGrid;
                itemGrid.SetItemGrid(EnumGrid.Fortify, 1);
                if (fortifyNum % 2 == 0)
                {
                    if (fortifyNum / 2 == rodLevel)
                    {
                        ShowFortify(fortifyNum - 2);
                        ShowFortify(fortifyNum - 1);
                    }
                }
                else
                {
                    ShowFortify(fortifyNum - 1);
                }
            });
        }
        private void ShowFortify(int fortifyIndex)
        {
            Debug.Log(fortifyIndex);
            ItemGrid itemGrid = fortifyIndex % 2 == 0 ? m_FortifyGrid1 : m_FortifyGrid2;
            itemGrid.AddItem(this, GameEntry.Data.GetData<DataSpoil>().GetFortify(fortifyIndex).ID);
        }
    }
}