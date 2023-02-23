using UnityEngine;
using UnityEngine.UI;
using System;
namespace Fishing
{
    public class Fish : ItemLogicEx
    {
        
        private float m_Radius, m_Speed, m_MoveStep;
        private Action m_CatchCallback;
        private UIFishingForm m_FishingForm;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            var fishParams = userData as FishParams;
            m_CatchCallback = fishParams.CatchCallback;
            fishParams.Clear();
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Catcher")
            {
                m_CatchCallback.Invoke();
            }
        }
    }
}