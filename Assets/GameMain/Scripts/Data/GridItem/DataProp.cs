using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Data;
using GameFramework.DataTable;
using System.Linq;

namespace Fishing.Data
{
    public sealed class DataProp : DataBase
    {
        private IDataTable<DRProp> dtProp;

        private Dictionary<int, PropData> dicPropData;
        private GameFrameworkMultiDictionary<int, PropData> m_LevelPropDic;

        protected override void OnInit()
        {

        }

        protected override void OnPreload()
        {
            LoadDataTable("Prop");
        }

        protected override void OnLoad()
        {
            dtProp = GameEntry.DataTable.GetDataTable<DRProp>();
            if (dtProp == null)
                throw new System.Exception("Can not get data table Item");

            dicPropData = new Dictionary<int, PropData>();
            m_LevelPropDic = new GameFrameworkMultiDictionary<int, PropData>();
            DRProp[] drProps = dtProp.GetAllDataRows();
            foreach (var drProp in drProps)
            {
                PropData propData = new PropData(drProp);
                dicPropData.Add(drProp.Id, propData);
                m_LevelPropDic.Add(propData.Level, propData);
            }
        }

        public PropData GetPropData(int id)
        {
            if (dicPropData.ContainsKey(id))
            {
                return dicPropData[id];
            }

            return null;
        }
        public PropData GetRandomPropDataByLevel(int Level)
        {
            return m_LevelPropDic[Level].ElementAt(Random.Range(0, m_LevelPropDic[Level].Count));
        }

        public PropData[] GetAllPropData()
        {
            int index = 0;
            PropData[] results = new PropData[dicPropData.Count];
            foreach (var propData in dicPropData.Values)
            {
                results[index++] = propData;
            }

            return results;
        }

        protected override void OnUnload()
        {
            GameEntry.DataTable.DestroyDataTable<DRProp>();
            dtProp = null;
            dicPropData = null;
        }

        protected override void OnShutdown()
        {
        }
    }

    public class PropData : IGridItemData
    {
        private DRProp dRProp;

        public int ID
        {
            get
            {
                return dRProp.Id;
            }
        }
        public string ItemName
        {
            get
            {
                return dRProp.Name;
            }
        }
        public int Level
        {
            get
            {
                return dRProp.Level;
            }
        }
        public int ImageID
        {
            get
            {
                return dRProp.ImageID;
            }
        }
        public string ItemType
        {
            get
            {
                return EnumItemType.Prop.ToString();
            }
        }
        public string ItemDescription
        {
            get
            {
                return dRProp.Description;
            }
        }
        public int Price
        {
            get
            {
                return dRProp.Price;
            }
        }
        public int Buff
        {
            get
            {
                return dRProp.Buff;
            }
        }
        public PropData(DRProp dRProp)
        {
            this.dRProp = dRProp;
        }
    }
}
