using UnityEngine;
using GameFramework.DataTable;
using GameFramework.Data;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
namespace Fishing.Data
{
    public class DataHelper : DataBase
    {
        private IDataTable<DRHelper> dtHelper;
        private Dictionary<int, HelperData> dicHelperData;
        protected override void OnPreload()
        {
            LoadDataTable("Helper");
        }

        protected override void OnLoad()
        {
            dtHelper = GameEntry.DataTable.GetDataTable<DRHelper>();
            if (dtHelper == null)
                throw new System.Exception("Can not get data table Item");

            dicHelperData = new Dictionary<int,HelperData>();
            DRHelper[] drHelpers = dtHelper.GetAllDataRows();
            foreach (var drHelper in drHelpers)
            {
                HelperData intDataData = new HelperData(drHelper);
                dicHelperData.Add(drHelper.Id, intDataData);
            }
        }
        public HelperData GetHelperData(int id)
        {
            if (dicHelperData.ContainsKey(id))
            {
                return dicHelperData[id];
            }
            return null;
        }

        public HelperData[] GetAllHelperData()
        {
            int index = 0;
            HelperData[] results = new HelperData[dicHelperData.Count];
            foreach (var buffData in dicHelperData.Values)
            {
                results[index++] = buffData;
            }

            return results;
        }

        protected override void OnUnload()
        {
            GameEntry.DataTable.DestroyDataTable<DRHelper>();
            dtHelper = null;
            dicHelperData = null;
        }
    }
    public class HelperData
    {
        private DRHelper dRHelper;
        public int ID
        {
            get
            {
                return dRHelper.Id;
            }
        }
        public string DataName
        {
            get
            {
                return dRHelper.DataName;
            }
        }
        public string Info
        {
            get
            {
                return dRHelper.Info;
            }
        }
        public HelperData(DRHelper dRHelper)
        {
            this.dRHelper = dRHelper;
        }
    }
}