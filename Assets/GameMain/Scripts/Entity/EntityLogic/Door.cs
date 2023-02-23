using UnityEngine;

namespace Fishing
{
    public class Door : NPCBase
    {
        protected override string InteractText{
            get{
                return m_InteractText;
            }
        }
        string m_InteractText;
        protected override void OnMorning()
        {
            UnregisterInteract();
            m_InteractText = "要睡这么早吗……";
        }
        protected override void OnFishing()
        {
            UnregisterInteract();
            m_InteractText = "体力还很充沛，不太想睡觉";
        }
        protected override void OnDusk()
        {
            RegisterInteract(Sleep);
            m_InteractText = "睡觉";
        }
        private void Sleep()
        {
            if(GameEntry.PlayerData.IsNeedSubmit)
            {
                GameEntry.UI.OpenTipsPopForm("你需要先提交今天的战利品");
            }
            else{
                GameEntry.Event.Fire(this, SleepEventArgs.Create());
            }
        }
    }
}