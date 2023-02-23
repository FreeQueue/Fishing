using UnityEngine;
using GameFramework.Event;
using Fishing.Data;
using DayOwner = GameFramework.Fsm.IFsm<Fishing.Timer.DayTimer>;
namespace Fishing.Timer
{
    public class DayDusk : DayStateBase
    {
        DayOwner dayOwner;
        public override int dayStateID
        {
            get
            {
                return 2;
            }
        }
        protected override void OnEnter(DayOwner dayOwner)
        {
            base.OnEnter(dayOwner);
            GameEntry.Event.Subscribe(SleepEventArgs.EventId, OnSleep);
            this.dayOwner = dayOwner;
        }
        protected override void OnLeave(DayOwner dayOwner, bool isShutdown)
        {
            base.OnLeave(dayOwner, isShutdown);
            GameEntry.Event.Unsubscribe(SleepEventArgs.EventId, OnSleep);
        }
        private void OnSleep(object sender, GameEventArgs e)
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UIDayChangeForm);
            GameEntry.PlayerData.ResetData(EnumIntData.Strength);
            GameEntry.ItemGrid.Save();
            GenerateNewProps();
            ChangeState<DayMorning>(dayOwner);
        }
        private void GenerateNewProps()
        {
            ItemGridGroupBase storeItemGroup = GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Store).m_ItemGridGroupBase;
            storeItemGroup.RemoveAllItem();
            int level = GameEntry.PlayerData.GetData(EnumIntData.RodLevel);
            DataProp dataProp = GameEntry.Data.GetData<DataProp>();
            for (int i = 0; i < 6; i++)
            {
                storeItemGroup.AddItem(dataProp.GetRandomPropDataByLevel(Mathf.Clamp(UnityEngine.Random.Range(level - 2, level + 1), 1, level)).ID);
            }
        }
    }
}