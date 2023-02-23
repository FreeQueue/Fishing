using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class UIItemTipsForm : UGuiFormEx
    {
        public RectTransform TextItem;
        public Text text;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            ItemTipsParams itemTipsParams = (ItemTipsParams)userData;
            text.text = itemTipsParams.ItemName;
            TextItem.anchoredPosition = Input.mousePosition;
            itemTipsParams.Clear();
        }
    }
}
