using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class UITipsPopForm : UGuiFormEx
    {
        [SerializeField]
        private Text Tips;
        [SerializeField]
        private float liveTime;
        private float timer;
        
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Tips.text = userData as string;
            timer = liveTime;
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            timer -= realElapseSeconds;
            if(timer<0)
            {
                Close();
            }
        }
    }
}