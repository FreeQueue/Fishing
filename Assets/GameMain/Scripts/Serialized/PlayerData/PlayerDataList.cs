using UnityEngine;
using GameFramework.Event;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using System;
using Fishing.Data;
namespace Fishing
{
    public class PlayerIntDataList
    {
        private int DataDefault
        {
            get
            {
                return GameEntry.Data.GetData<DataIntData>().GetIntDataData(m_currentData).DefaultValue;
            }
        }
        private EnumIntData m_currentData;
        private int IntData
        {
            get
            {
                if (!GameEntry.Setting.HasSetting("Player." + m_currentData))
                {
                    GameEntry.Setting.SetInt("Player." + m_currentData, DataDefault);
                }
                return GameEntry.Setting.GetInt("Player." + m_currentData);
            }
            set
            {
                GameEntry.Setting.SetInt("Player." + m_currentData, value);
                Debug.Log($"{m_currentData}:{value}");
                GameEntry.Event.Fire(this, PlayerDataChangeEventArgs.Create(m_currentData.ToString(), value));
            }
        }
        public int GetData(EnumIntData dataName)
        {
            m_currentData = dataName;
            return IntData;
        }
        public int GetDataDefault(EnumIntData dataName)
        {
            m_currentData = dataName;
            return DataDefault;
        }
        public bool IsDataDefault(EnumIntData dataName)
        {
            m_currentData = dataName;
            return DataDefault==IntData;
        }
        public void SetData(EnumIntData dataName, int value)
        {
            m_currentData = dataName;
            IntData = value;
        }
        public void ChangeData(EnumIntData dataName, int value)
        {
            m_currentData = dataName;
            IntData += value;
        }
        public void ResetData(EnumIntData dataName)
        {
            m_currentData = dataName;
            IntData = DataDefault;
        }
        public void Reset()
        {
            foreach (EnumIntData enumData in Enum.GetValues(typeof(EnumIntData)))
            {
                if(enumData!=EnumIntData.None)
                ResetData(enumData);
            }
        }
    }
    public class PlayerBoolDataList
    {
        private bool m_DataDefault;
        private string m_currentData;
        private bool BoolData
        {
            get
            {
                if (!GameEntry.Setting.HasSetting("Player." + m_currentData))
                {
                    GameEntry.Setting.SetBool("Player." + m_currentData, m_DataDefault);
                }
                return GameEntry.Setting.GetBool("Player." + m_currentData);
            }
            set
            {
                GameEntry.Setting.SetBool("Player." + m_currentData, value);
                GameEntry.Event.Fire(this, PlayerDataChangeEventArgs.Create(m_currentData, value ? 1 : 0));
            }
        }

        public bool GetData(EnumBoolData dataName)
        {
            m_currentData = dataName.ToString();
            m_DataDefault = (int)dataName == 1;
            return BoolData;
        }
        public void SetData(EnumBoolData dataName, bool value)
        {
            m_currentData = dataName.ToString();
            m_DataDefault = (int)dataName == 1;
            BoolData = value;
        }
        public void Reset()
        {
            foreach (EnumBoolData dataName in Enum.GetValues(typeof(EnumBoolData)))
            {
                SetData(dataName, (int)dataName == 1);
            }
        }
    }
}