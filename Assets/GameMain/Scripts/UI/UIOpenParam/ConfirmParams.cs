using GameFramework;
using UnityEngine.Events;
namespace Fishing
{
    public class ConfirmParams : IReference
    {
        public string Question
        {
            get;
            private set;
        }
        public UnityAction OnConfirmCallback
        {
            get;
            private set;
        }
        public UnityAction OnCancelCallback
        {
            get;
            private set;
        }
        public static ConfirmParams Create(string question, UnityAction onConfirmCallback, UnityAction onCancelCallback)
        {
            ConfirmParams confirmParams = ReferencePool.Acquire<ConfirmParams>();
            confirmParams.Question = question;
            confirmParams.OnConfirmCallback = onConfirmCallback;
            confirmParams.OnCancelCallback = onCancelCallback;
            return confirmParams;
        }
        public void Clear()
        {
            OnConfirmCallback = null;
            OnCancelCallback = null;
        }
    }
}
