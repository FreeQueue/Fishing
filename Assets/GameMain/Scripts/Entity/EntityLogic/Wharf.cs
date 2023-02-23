using UnityEngine;
using GameFramework.Event;
namespace Fishing
{
    public class Wharf : NPCBase
    {
        protected override string InteractText
        {
            get
            {
                return m_InteractText;
            }
        }
        string m_InteractText;
        protected override void OnMorning()
        {

            if (GameEntry.PlayerData.IsNeedUpgrade)
            {
                m_InteractText = "万启好像有事找你";
            }
            else
            {
                RegisterInteract(StartFishingTime);
                m_InteractText = "开始钓鱼";
            }
        }
        protected override void OnFishing()
        {
            RegisterInteract(StartFishing);
            m_InteractText = "开始钓鱼";
        }
        protected override void OnDusk()
        {
            UnregisterInteract();
            m_InteractText = "现在不能钓鱼，该休息了";
        }
        private void StartFishingTime()
        {
            GameEntry.UI.OpenConfirmForm("要开始钓鱼吗，将会进入垂钓时间", () =>
            {
                GameEntry.Event.Fire(this, StartFishingTimeEventArgs.Create());
                StartFishing();
            }, null);
        }
        private void StartFishing()
        {
            if(GameEntry.PlayerData.GetData(EnumIntData.Strength)<=0){
                GameEntry.UI.OpenTipsForm("已经没有体力了……", null);
                return;
            }
            GameEntry.Event.Fire(this, StartFishingEventArgs.Create());
        }
    }
}