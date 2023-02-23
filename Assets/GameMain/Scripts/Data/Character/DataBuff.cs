using UnityEngine;
using GameFramework.DataTable;
using GameFramework.Data;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
namespace Fishing.Data
{
    public class DataBuff : DataBase
    {
        private IDataTable<DRBuff> dtBuff;
        private Dictionary<int, BuffData> dicBuffData;
        private Dictionary<string, BuffData> m_NameDicBuffData;
        protected override void OnPreload()
        {
            LoadDataTable("Buff");
        }
        protected override void OnLoad()
        {
            dtBuff = GameEntry.DataTable.GetDataTable<DRBuff>();
            if (dtBuff == null)
                throw new System.Exception("Can not get data table Item");

            dicBuffData = new Dictionary<int,BuffData>();
            m_NameDicBuffData = new Dictionary<string, BuffData>();
            DRBuff[] drBuffs = dtBuff.GetAllDataRows();
            foreach (var drBuff in drBuffs)
            {
                BuffData buffData = new BuffData(drBuff);
                dicBuffData.Add(drBuff.Id, buffData);
                m_NameDicBuffData.Add(drBuff.Effect, buffData);
            }
        }
        public BuffData GetBuffData(int id)
        {
            if (dicBuffData.ContainsKey(id))
            {
                return dicBuffData[id];
            }
            return null;
        }
        public BuffData GetBuffDataByEnum(EnumBuff buffEffect)
        {
            if (m_NameDicBuffData.ContainsKey(buffEffect.ToString()))
            {
                return m_NameDicBuffData[buffEffect.ToString()];
            }
            return null;
        }
        public BuffData[] GetAllBuffData()
        {
            int index = 0;
            BuffData[] results = new BuffData[dicBuffData.Count];
            foreach (var buffData in dicBuffData.Values)
            {
                results[index++] = buffData;
            }

            return results;
        }

        protected override void OnUnload()
        {
            GameEntry.DataTable.DestroyDataTable<DRBuff>();
            dtBuff = null;
            dicBuffData = null;
        }
    }
    public class BuffData
    {
        private DRBuff dRBuff;
        public int ID
        {
            get
            {
                return dRBuff.Id;
            }
        }
        public string Effect
        {
            get
            {
                return dRBuff.Effect;
            }
        }
        public int ImageID
        {
            get
            {
                return dRBuff.ImageID;
            }
        }
        public string Description
        {
            get
            {
                return dRBuff.Description;
            }
        }
        public int GetLevelValue(int level)
        {
            return dRBuff.GetLevelAt(level);
        }
        public BuffData(DRBuff dRBuff)
        {
            this.dRBuff = dRBuff;
        }
    }
}