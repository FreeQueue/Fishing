using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class UIInteractForm : UGuiFormEx
    {
        [SerializeField]
        private Text text;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            InteractParams interactParams = userData as InteractParams;
            text.text = interactParams.Tips;
        }
    }
}