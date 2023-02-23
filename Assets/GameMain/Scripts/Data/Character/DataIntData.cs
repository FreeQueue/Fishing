using UnityEngine;
using GameFramework.DataTable;
using GameFramework.Data;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
namespace Fishing.Data
{
    public class DataIntData : DataBase
    {
        private IDataTable<DRIntData> dtIntData;
        private Dictionary<int, IntDataData> dicIntDataData;
        protected override void OnPreload()
        {
            LoadDataTable("IntData");
        }

        protected override void OnLoad()
        {
            dtIntData = GameEntry.DataTable.GetDataTable<DRIntData>();
            if (dtIntData == null)
                throw new System.Exception("Can not get data table Item");

            dicIntDataData = new Dictionary<int,IntDataData>();
            DRIntData[] drIntDatas = dtIntData.GetAllDataRows();
            foreach (var drIntData in drIntDatas)
            {
                IntDataData intDataData = new IntDataData(drIntData);
                dicIntDataData.Add(drIntData.Id, intDataData);
            }
        }
        public IntDataData GetIntDataData(int id)
        {
            if (dicIntDataData.ContainsKey(id))
            {
                return dicIntDataData[id];
            }
            return null;
        }
        public IntDataData GetIntDataData(EnumIntData enumIntData)
        {
            return GetIntDataData((int)enumIntData);
        }
        public IntDataData[] GetAllIntDataData()
        {
            int index = 0;
            IntDataData[] results = new IntDataData[dicIntDataData.Count];
            foreach (var buffData in dicIntDataData.Values)
            {
                results[index++] = buffData;
            }

            return results;
        }

        protected override void OnUnload()
        {
            GameEntry.DataTable.DestroyDataTable<DRIntData>();
            dtIntData = null;
            dicIntDataData = null;
        }
    }
    public class IntDataData
    {
        private DRIntData dRIntData;
        public int ID
        {
            get
            {
                return dRIntData.Id;
            }
        }
        public string DataName
        {
            get
            {
                return dRIntData.DataName;
            }
        }
        public int DefaultValue
        {
            get
            {
                return dRIntData.DefaultValue;
            }
        }
        public IntDataData(DRIntData dRIntData)
        {
            this.dRIntData = dRIntData;
        }
    }
}