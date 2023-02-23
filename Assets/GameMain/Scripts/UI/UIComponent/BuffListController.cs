using UnityEngine;
using System;
using GameFramework.Event;
using GameFramework;
using Fishing.Data;
using System.Collections.Generic;
namespace Fishing
{
    public class BuffListController
    {
        private DataBuff dataBuff;
        private UGuiFormEx m_UGuiFormEx;
        private Transform m_BuffListRoot;
        private Dictionary<EnumBuff, BuffBar> m_BuffBarDic;
        public BuffListController(UGuiFormEx uGuiFormEx, Transform buffListRoot)
        {
            m_BuffBarDic = new Dictionary<EnumBuff, BuffBar>();
            m_UGuiFormEx = uGuiFormEx;
            m_BuffListRoot = buffListRoot;
        }
        public void ShowBuffList()
        {
            m_BuffBarDic.Clear();
            dataBuff = GameEntry.Data.GetData<DataBuff>();
            foreach (EnumBuff item in Enum.GetValues(typeof(EnumBuff)))
            {
                if (item == EnumBuff.None || item == EnumBuff.Clear) continue;
                ShowBuffBar(item);
                Subscribe(item);
            }
        }
        private void ShowBuffBar(EnumBuff enumBuff)
        {
            ShowBuffBar(enumBuff, GameEntry.PlayerData.GetData(enumBuff.ToIntData()));
        }
        private void Subscribe(EnumBuff enumBuff)
        {
            m_UGuiFormEx.Subscribe(PlayerDataChangeEventArgs.EventId(enumBuff.ToIntData()), (sender, e) =>
            {
                PlayerDataChangeEventArgs ne = e as PlayerDataChangeEventArgs;
                int data = (int)ne.Data;
                ShowBuffBar((EnumBuff)Enum.Parse(typeof(EnumBuff), ne.DataName), data);
            });
        }
        private void ShowBuffBar(EnumBuff enumBuff, int data)
        {
            if (m_BuffBarDic.ContainsKey(enumBuff))
            {
                m_UGuiFormEx.HideItem(m_BuffBarDic[enumBuff].Item);
                m_BuffBarDic.Remove(enumBuff);
            }
            
            if (data <= GameEntry.PlayerData.GetDataDefault(enumBuff.ToIntData())) return;
            BuffData buffData = dataBuff.GetBuffDataByEnum(enumBuff);
            m_UGuiFormEx.ShowItem<BuffBar>(EnumItem.BuffBar, (item) =>
            {
                item.transform.SetParent(m_BuffListRoot);
                m_BuffBarDic[enumBuff] = item.Logic as BuffBar;
            },
            BuffParams.Create(buffData.ImageID, buffData.Effect,
            Utility.Text.Format(buffData.Description, data)));
        }
    }
}