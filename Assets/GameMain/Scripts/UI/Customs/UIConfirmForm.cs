using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class UIConfirmForm : UGuiFormEx
    {
        [SerializeField]
        private Button confirmButton, cancelButton;
        [SerializeField]
        private Text Question;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            ConfirmParams confirmParams = userData as ConfirmParams;
            Question.text = confirmParams.Question;
            if(confirmParams.OnConfirmCallback!=null)
            confirmButton.onClick.AddListener(confirmParams.OnConfirmCallback);
            if(confirmParams.OnCancelCallback!=null)
            cancelButton.onClick.AddListener(confirmParams.OnCancelCallback);

            confirmButton.onClick.AddListener(OnAnyButtonClick);
            cancelButton.onClick.AddListener(OnAnyButtonClick);
            confirmParams.Clear();
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            confirmButton.onClick.RemoveAllListeners();
            cancelButton.onClick.RemoveAllListeners();
        }
        private void OnAnyButtonClick()
        {
            Close();
        }
    }
}
