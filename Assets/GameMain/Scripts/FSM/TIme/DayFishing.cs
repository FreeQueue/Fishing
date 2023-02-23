using UnityEngine;
using GameFramework.Event;
using DayOwner = GameFramework.Fsm.IFsm<Fishing.Timer.DayTimer>;
namespace Fishing.Timer
{
    public class DayFishing : DayStateBase
    {
        public override int dayStateID
        {
            get
            {
                return 1;
            }
        }
        DayOwner dayOwner;
        protected override void OnEnter(DayOwner dayOwner)
        {
            base.OnEnter(dayOwner);
            this.dayOwner = dayOwner;
            GameEntry.Event.Subscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.Strength), OnLeaveFishingTime);
        }
        protected override void OnLeave(DayOwner dayOwner, bool isShutdown)
        {
            base.OnLeave(dayOwner, isShutdown);
            GameEntry.Event.Unsubscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.Strength), OnLeaveFishingTime);
        }

        private void OnLeaveFishingTime(object sender, GameEventArgs e)
        {
            PlayerDataChangeEventArgs ne = e as PlayerDataChangeEventArgs;
            if ((int)ne.Data <= 0)
            {
                ChangeState<DayDusk>(dayOwner);
            }
        }
    }
}