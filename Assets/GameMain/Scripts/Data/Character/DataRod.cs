using UnityEngine;
using GameFramework.DataTable;
using GameFramework.Data;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
namespace Fishing.Data
{
    public class DataRod : DataBase
    {

        private IDataTable<DRRod> dtRod;

        private Dictionary<int, RodData> dicRodData;

        protected override void OnInit()
        {

        }

        protected override void OnPreload()
        {
            LoadDataTable("Rod");
        }

        protected override void OnLoad()
        {
            dtRod = GameEntry.DataTable.GetDataTable<DRRod>();
            if (dtRod == null)
                throw new System.Exception("Can not get data table Item");

            dicRodData = new Dictionary<int, RodData>();

            DRRod[] drRods = dtRod.GetAllDataRows();
            foreach (var drRod in drRods)
            {
                RodData rodData = new RodData(drRod);
                dicRodData.Add(drRod.Level, rodData);
            }
        }
        public RodData GetRodDataByLevel(int level)
        {
            if (dicRodData.ContainsKey(level))
            {
                return dicRodData[level];
            }
            return null;
        }

        public RodData[] GetAllRodData()
        {
            int index = 0;
            RodData[] results = new RodData[dicRodData.Count];
            foreach (var rodData in dicRodData.Values)
            {
                results[index++] = rodData;
            }

            return results;
        }

        protected override void OnUnload()
        {
            GameEntry.DataTable.DestroyDataTable<DRRod>();
            dtRod = null;
            dicRodData = null;
        }
    }
    public class RodData
    {
        private DRRod dRRod;
        public int ID
        {
            get
            {
                return dRRod.Id;
            }
        }
        public int Level
        {
            get
            {
                return dRRod.Level;
            }
        }
        public int Percentage
        {
            get
            {
                return dRRod.Percentage;
            }
        }
        public int CatcherScale
        {
            get
            {
                return dRRod.CatcherScale;
            }
        }
        public int ExtraChance
        {
            get
            {
                return dRRod.ExtraChance;
            }
        }
        public int GetRandomSpoilLevel()
        {
            int random = Random.Range(0, 100);
            for (int i = 1; i <= 5; i++)
            {
                if (random < GetLevelValue(i))
                {
                    return i;
                }
                else
                {
                    random -= GetLevelValue(i);
                }
            }
            return 1;
        }
        public int GetLevelValue(int level)
        {
            if (level < 1 || level > 5) { Log.Error($"Level value: {level} is invalid"); }
            switch (level)
            {
                case 1: return dRRod.Level1;
                case 2: return dRRod.Level2;
                case 3: return dRRod.Level3;
                case 4: return dRRod.Level4;
                case 5: return dRRod.Level5;
                default: return 0;
            }
        }
        public RodData(DRRod dRRod)
        {
            this.dRRod = dRRod;
        }
    }
}