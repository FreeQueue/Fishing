using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using System;
using Fishing.Data;
namespace Fishing
{
    public class ItemGrid : ItemLogicEx, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private Image Background, Frame;
        protected virtual Action OnClick
        {
            get
            {
                switch (GridGroupType)
                {
                    case EnumGrid.Bag:
                    case EnumGrid.Fortify:
                    case EnumGrid.Relic:
                    default:
                        return () =>
                        {
                            if (!IsEmpty)
                                GameEntry.UI.OpenUIForm(EnumUIForm.UIItemDescriptionForm, GridItem);
                        };
                    case EnumGrid.Prop:
                        return () =>
                        {
                            if (!IsEmpty)
                                GameEntry.UI.OpenUIForm(EnumUIForm.UIItemUseForm, UseItemParams.Create(GridItem, UseItem));
                        };
                }
            }
        }
        protected virtual Action OnSelect
        {
            get
            {
                return () =>
                {
                    if (!IsEmpty)
                    {
                        Background.sprite = GameEntry.ItemGrid.GridImage.GetImage((int)EnumGridImage.Select);
                        GameEntry.Sound.PlayMusic(EnumSound.菜单1);
                        StartCoroutine("PopTipsCo");
                    }
                };
            }
        }
        protected virtual Action OnSelectOut
        {
            get
            {
                return () =>
                {
                    if (!IsEmpty)
                    {
                        SetNormal();
                        StopCoroutine("PopTipsCo");
                        if (TipsID != null)
                        {
                            GameEntry.UI.CloseUIForm((int)TipsID);
                            TipsID = null;
                        }
                    }
                };
            }
        }
        protected virtual Action OnNormal
        {
            get
            {
                return () => { };
            }
        }
        protected virtual Action OnEmpty
        {
            get
            {
                switch (GridGroupType)
                {
                    case EnumGrid.Bag:
                    case EnumGrid.Prop:
                    default:
                        return () =>
                        {
                            Background.sprite = GameEntry.ItemGrid.GridImage.GetImage((int)EnumGridImage.Disable);
                        };
                    case EnumGrid.Fortify:
                    case EnumGrid.Relic:
                        return () =>
                            {
                                Background.sprite = GameEntry.ItemGrid.GridImage.GetImage((int)EnumGridImage.Unknown);
                            };
                }
            }
        }
        public bool IsEmpty
        {
            get
            {
                return GridItem == null;
            }
        }
        public EnumGrid GridGroupType
        {
            get;
            private set;
        }
        public int GridID
        {
            get;
            private set;
        }
        private GridItem m_GridItem;
        public GridItem GridItem
        {
            get => m_GridItem;
            protected set
            {
                m_GridItem = value;
                Refresh();
            }
        }
        private int? TipsID;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            Refresh();
        }
        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);

            SetEmpty();
            StopCoroutine("PopTipsCo");
            if (TipsID != null)
            {
                GameEntry.UI.CloseUIForm((int)TipsID);
                TipsID = null;
            }
        }
        public void Refresh()
        {
            if (IsEmpty) SetEmpty();
            else SetNormal();
        }
        public void SetItemGrid(EnumGrid enumGrid, int gridID)
        {
            GridGroupType = enumGrid;
            GridID = gridID;
            Refresh();
        }
        public void AddItem(UGuiFormEx uGuiFormEx, int itemID)
        {
            uGuiFormEx.ShowItem<GridItem>(EnumItem.GridItem, (item) =>
            {
                item.transform.SetParent(transform, false);
                item.transform.localPosition = Vector3.zero;
                m_GridItem = item.Logic as GridItem;
                m_GridItem.SetItemData(itemID);
                SetNormal();
            });
        }
        public void RemoveItem(UGuiFormEx uGuiFormEx)
        {
            if (IsEmpty)
            {
                Log.Error("There is no item to remove");
                return;
            }
            uGuiFormEx.HideItem(GridItem.Item);
            GridItem = null;
        }
        private void SetNormal()
        {
            Background.sprite = GameEntry.ItemGrid.GridImage.GetImage((int)EnumGridImage.Default);
            Frame.color = GameEntry.ItemGrid.LevelColor.GetLevelColor(GridItem.Level);
            OnNormal.Invoke();
        }
        private void SetEmpty()
        {
            Frame.color = GameEntry.ItemGrid.LevelColor.GetLevelColor(0);
            OnEmpty.Invoke();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnSelect.Invoke();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            OnSelectOut.Invoke();
        }
        public IEnumerator PopTipsCo()
        {
            float timer = 0.5f;
            while (timer > 0)
            {
                timer -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            if (!IsEmpty)
                TipsID = GameEntry.UI.OpenUIForm(EnumUIForm.UIItemTipsForm, ItemTipsParams.Create(GridItem));
        }
        public void UseItem()
        {
            GameEntry.Event.Fire(this, UseItemEventArgs.Create());
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClick.Invoke();
        }
    }
}
