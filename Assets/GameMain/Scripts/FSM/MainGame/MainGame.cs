using UnityEngine;
using GameFramework.Event;
using GameFramework.Fsm;
namespace Fishing
{
    public class MainGame : FsmState<MainController>
    {
        FishingController fishingController = new FishingController();
        private int? MainGameUI;
        IFsm<MainController> fsm;
        protected override void OnEnter(IFsm<MainController> fsm)
        {
            this.fsm = fsm;
            fsm.Owner.dayTimer.StartDayTimer();
            fsm.Owner.m_BuffController.Start();
            MainGameUI = GameEntry.UI.OpenUIForm(EnumUIForm.UIMainGameForm);
            GameEntry.Event.Subscribe(StartFishingEventArgs.EventId, OnStartFishing);
        }

        protected override void OnLeave(IFsm<MainController> fsm, bool isShutdown)
        {
            
            GameEntry.Event.Unsubscribe(StartFishingEventArgs.EventId, OnStartFishing);
            fsm.Owner.dayTimer.ShutdownDayTimer();
            fsm.Owner.m_BuffController.Shutdown();
            if (MainGameUI != null)
                GameEntry.UI.CloseUIForm((int)MainGameUI);
        }
        private void OnStartFishing(object sender, GameEventArgs e)
        {

            fishingController.Start();
            GameEntry.Event.Subscribe(FishingFinishEventArgs.EventId, OnFishingFinish);
        }
        private void OnFishingFinish(object sender, GameEventArgs e)
        {
            GameEntry.Event.Unsubscribe(FishingFinishEventArgs.EventId, OnFishingFinish);
            fishingController.Shutdown();
        }
    }
}