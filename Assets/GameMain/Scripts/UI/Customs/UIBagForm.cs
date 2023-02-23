using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Fishing.Data;
namespace Fishing
{
    public class UIBagForm : UGuiFormEx
    {
        [SerializeField]
        private Toggle spoilToggle, propToggle;
        [SerializeField]
        private Button closeButton;
        [SerializeField]
        private RectTransform m_SpoilGridRoot, m_PropGridRoot, m_BuffListRoot;
        private GridController m_SpoilGridController, m_PropGridController;
        private BuffListController m_BuffListController;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_SpoilGridController = new GridController(this, EnumGrid.Bag, m_SpoilGridRoot);
            m_PropGridController = new GridController(this, EnumGrid.Prop, m_PropGridRoot);
            m_BuffListController = new BuffListController(this, m_BuffListRoot);
            spoilToggle.onValueChanged.AddListener(OnSpoilToggleChange);
            propToggle.onValueChanged.AddListener(OnPropToggleChange);
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            spoilToggle.isOn = true;
            Register(InputSys.EnumInput.Cancel, OnCloseButtonClick);
            Subscribe(AddItemEventArgs.EventId, m_SpoilGridController.OnAddItem);
            Subscribe(AddItemEventArgs.EventId, m_PropGridController.OnAddItem);
            Subscribe(RemoveItemEventArgs.EventId, m_SpoilGridController.OnRemoveItem);
            Subscribe(RemoveItemEventArgs.EventId, m_PropGridController.OnRemoveItem);
            m_SpoilGridController.ShowItemGrids();
            m_PropGridController.ShowItemGrids();
            m_BuffListController.ShowBuffList();
        }
        private void OnCloseButtonClick()
        {
            Close();
        }
        private void OnSpoilToggleChange(bool value)
        {
            m_SpoilGridRoot.gameObject.SetActive(value);
        }
        private void OnPropToggleChange(bool value)
        {
            m_PropGridRoot.gameObject.SetActive(value);
        }
    }
}
