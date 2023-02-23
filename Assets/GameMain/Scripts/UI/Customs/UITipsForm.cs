using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class UITipsForm : UGuiFormEx
    {
        [SerializeField]
        private Button confirmButton;
        [SerializeField]
        private Text Tips;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            ConfirmParams confirmParams = userData as ConfirmParams;
            Tips.text = confirmParams.Question;
            if(confirmParams.OnConfirmCallback!=null)
            confirmButton.onClick.AddListener(confirmParams.OnConfirmCallback);
            confirmButton.onClick.AddListener(OnAnyButtonClick);
            confirmParams.Clear();
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            confirmButton.onClick.RemoveAllListeners();
        }
        private void OnAnyButtonClick()
        {
            Close();
        }
    }
}
