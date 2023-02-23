using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class BuffBar : ItemLogicEx
    {
        [SerializeField]
        Image m_Icon;
        [SerializeField]
        Text m_BuffName, m_Description;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            BuffParams buffParams = userData as BuffParams;
            if(buffParams==null )Debug.Log("cnm");
            Refresh(buffParams);
        }
        private void Refresh(BuffParams buffParams)
        {
            m_Icon.sprite = GameEntry.ItemGrid.ItemImage.GetImage(buffParams.ImageID);
            m_BuffName.text = buffParams.Name;
            m_Description.text = buffParams.Description;
        }
    }
}