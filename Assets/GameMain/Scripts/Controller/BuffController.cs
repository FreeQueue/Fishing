using System;
using Fishing.Data;
using GameFramework;
using UnityGameFramework.Runtime;
namespace Fishing
{
    public class BuffController
    {
        public bool IsNoBuff
        {
            get
            {
                foreach (EnumBuff item in Enum.GetValues(typeof(EnumBuff)))
                {
                    if (item == EnumBuff.Clear || item == EnumBuff.None) continue;
                    if (!GameEntry.PlayerData.IsDataDefault(item.ToIntData())) return false;
                }
                return true;
            }
        }
        private DataBuff m_DataBuff;
        public void Start()
        {
            m_DataBuff = GameEntry.Data.GetData<DataBuff>();
            GameEntry.Event.Subscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.Day), OnDayChange);
            GameEntry.Event.Subscribe(UseItemEventArgs.EventId, UseProp);
        }
        public void Shutdown()
        {
            GameEntry.Event.Unsubscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.Day), OnDayChange);
            GameEntry.Event.Unsubscribe(UseItemEventArgs.EventId, UseProp);
        }
        private void OnDayChange(object sender, GameFrameworkEventArgs e)
        {
            Clear();
        }
        public void UseProp(object sender, GameFrameworkEventArgs e)
        {
            ItemGrid itemGrid = sender as ItemGrid;
            PropData propData = GameEntry.Data.GetData<DataProp>().GetPropData(itemGrid.GridItem.ID);
            BuffData buffData = m_DataBuff.GetBuffData(propData.Buff);
            EnumBuff enumBuff = (EnumBuff)Enum.Parse(typeof(EnumBuff), buffData.Effect);
            if (enumBuff == EnumBuff.Clear)
            {
                if (IsNoBuff) GameEntry.UI.OpenConfirmForm("现在使用不会有任何效果，要这么壕气吗？", Clear, null);
                return;
            }
            int value = buffData.GetLevelValue(propData.Level);
            if (value <= GameEntry.PlayerData.GetData(enumBuff.ToIntData()))
            {
                GameEntry.UI.OpenConfirmForm("现在使用不会有任何效果，要这么壕气吗？", () =>
                {
                    SetBuff(enumBuff, value);
                    GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Prop).m_ItemGridGroupBase.RemoveItemFromGrid(itemGrid.GridID);
                }, null);
                return;
            }
            SetBuff(enumBuff, value);
            GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Prop).m_ItemGridGroupBase.RemoveItemFromGrid(itemGrid.GridID);
        }
        private void SetBuff(EnumBuff enumBuff, int value)
        {
            GameEntry.PlayerData.SetData((EnumIntData)Enum.Parse(typeof(EnumIntData), enumBuff.ToString()), value);
        }
        public void Clear()
        {
            foreach (EnumBuff item in Enum.GetValues(typeof(EnumBuff)))
            {
                if (item == EnumBuff.Clear || item == EnumBuff.None) continue;
                GameEntry.PlayerData.ResetData(item.ToIntData());
            }
        }
    }
}