using UnityEngine.UI;
using UnityEngine;
using System;
namespace Fishing
{
    public class UIItemUseForm : UGuiFormEx
    {
        [SerializeField]
        private Text m_ItemName, m_ItemType, m_ItemDescription;
        [SerializeField]
        private Image m_Image;
        [SerializeField]
        private Button m_CloseButton,m_UseButton;
        private Action m_UseCallback;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_CloseButton.onClick.AddListener(OnCloseButtonClick);
            m_UseButton.onClick.AddListener(OnUseButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Register(InputSys.EnumInput.Cancel, OnCloseButtonClick);
            UseItemParams useItemParams =userData as UseItemParams;
            m_ItemName.text = useItemParams.GridItem.ItemName;
            m_ItemType.text = useItemParams.GridItem.ItemType;
            m_ItemDescription.text = useItemParams.GridItem.ItemDescription;
            m_Image.sprite = useItemParams.GridItem.Image.sprite;
            m_UseCallback = useItemParams.UseCallback;
        }
        private void OnUseButtonClick()
        {
            m_UseCallback.Invoke();
            Close();
        }
        private void OnCloseButtonClick()
        {
            Close();
        }
    }
}
