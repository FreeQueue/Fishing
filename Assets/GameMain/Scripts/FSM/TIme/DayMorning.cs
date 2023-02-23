using UnityEngine;
using GameFramework.Event;
using DayOwner = GameFramework.Fsm.IFsm<Fishing.Timer.DayTimer>;
namespace Fishing.Timer
{
    public class DayMorning : DayStateBase
    {
        public  override int dayStateID
        {
            get
            {
                return 0;
            }
        }
        DayOwner dayOwner;
        protected override void OnEnter(DayOwner dayOwner)
        {
            base.OnEnter(dayOwner);
            this.dayOwner = dayOwner;
            GameEntry.Event.Subscribe(StartFishingTimeEventArgs.EventId,OnStartFishingTime);
        }
        protected override void OnLeave(DayOwner dayOwner, bool isShutdown)
        {
            base.OnLeave(dayOwner, isShutdown);
            GameEntry.Event.Unsubscribe(StartFishingTimeEventArgs.EventId,OnStartFishingTime);
        }
        private void OnStartFishingTime(object sender,GameEventArgs e)
        {
            ChangeState<DayFishing>(dayOwner);
        }
    }
}