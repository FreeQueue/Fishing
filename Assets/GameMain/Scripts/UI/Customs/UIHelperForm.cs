using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using Fishing.Data;
namespace Fishing
{
    public class UIHelperForm : UGuiFormEx
    {
        [SerializeField]
        private Button m_CloseButton;
        [SerializeField]
        private ToggleGroup toggleGroup;
        [SerializeField]
        private Text m_Title,m_Info;
        private DataHelper m_DataHelper;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_DataHelper= GameEntry.Data.GetData<Data.DataHelper>();
            Bind();
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Register(InputSys.EnumInput.Cancel, OnCloseButtonClick);
            m_CloseButton.onClick.AddListener(OnCloseButtonClick);
            toggleGroup.GetComponentInChildren<Toggle>().isOn = true;
        }
        private void Bind(){
            int i = 1;
            foreach (var item in toggleGroup.GetComponentsInChildren<Toggle>())
            {
                item.onValueChanged.AddListener(SetText(i));
                i++;
            } 
        }
        private UnityAction<bool> SetText(int index){
            return (value) =>
            {
                HelperData helperData = m_DataHelper.GetHelperData(index);
                m_Title.text = helperData.DataName;
                m_Info.text = helperData.Info;
            };
        }
        private void OnCloseButtonClick()
        {
            Close();
        }
    }
}