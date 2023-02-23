using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Data;
using GameFramework.DataTable;

namespace Fishing.Data
{
    public sealed class DataSpoil : DataBase
    {
        private IDataTable<DRSpoil> dtSpoil;

        private Dictionary<int, SpoilData> dicSpoilData;
        private GameFrameworkMultiDictionary<int, SpoilData> m_LevelMineralDic;
        private int RelicIndex
        {
            get
            {
                return GameEntry.PlayerData.GetData(EnumIntData.RelicNum);
            }
            set
            {
                GameEntry.PlayerData.SetData(EnumIntData.RelicNum, value);
            }
        }
        public int RelicLevel
        {
            get
            {
                return RelicIndex / 2;
            }
        }
        private int FortifyIndex
        {
            get
            {
                return GameEntry.PlayerData.GetData(EnumIntData.FortifyNum);
            }
            set
            {
                GameEntry.PlayerData.SetData(EnumIntData.FortifyNum, value);
            }
        }
        public int FortifyLevel
        {
            get
            {
                return FortifyIndex / 2;
            }
        }
        private List<SpoilData> m_RelicList, m_FortifyList;
        protected override void OnInit()
        {

        }

        protected override void OnPreload()
        {
            LoadDataTable("Spoil");
        }
        protected override void OnLoad()
        {
            dtSpoil = GameEntry.DataTable.GetDataTable<DRSpoil>();
            if (dtSpoil == null)
                throw new System.Exception("Can not get data table Item");

            dicSpoilData = new Dictionary<int, SpoilData>();
            m_FortifyList = new List<SpoilData>();
            m_RelicList = new List<SpoilData>();
            m_LevelMineralDic = new GameFrameworkMultiDictionary<int, SpoilData>();
            DRSpoil[] drSpoils = dtSpoil.GetAllDataRows();
            foreach (var drSpoil in drSpoils)
            {
                SpoilData spoilData = new SpoilData(drSpoil);
                dicSpoilData.Add(drSpoil.Id, spoilData);
                if (spoilData.ItemType == EnumItemType.Mineral.ToString()) m_LevelMineralDic.Add(spoilData.Level, spoilData);
                if (spoilData.ItemType == EnumItemType.Relic.ToString()) m_RelicList.Add(spoilData);
                if (spoilData.ItemType == EnumItemType.Fortify.ToString()) m_FortifyList.Add(spoilData);
            }
        }
        public SpoilData GetRelic(int relicIndex)
        {
            return m_RelicList[relicIndex];
        }
        public SpoilData GetFortify(int fortifyIndex)
        {
            return m_FortifyList[fortifyIndex];
        }
        public SpoilData GetRandomMineral(int level)
        {
            int n = Random.Range(0, m_LevelMineralDic[level].Count);
            LinkedListNode<SpoilData> node = m_LevelMineralDic[level].First;
            for (int i = 0; i < n; i++)
            {
                node = node.Next;
            }
            return node.Value;
        }
        public SpoilData GetRelicOrFortify(int level)
        {
            int relicNum = level * 2 - RelicIndex;
            int fortifyNum = level * 2 - FortifyIndex;
            if (relicNum + fortifyNum < 1) return GetRandomMineral(level);
            int rule = Random.Range(-relicNum, fortifyNum);
            SpoilData spoilData;
            if (rule < 0)
            {
                spoilData = m_RelicList[RelicIndex];
                RelicIndex++;
            }
            else
            {
                spoilData = m_FortifyList[FortifyIndex];
                FortifyIndex++;
            }
            return spoilData;
        }
        public SpoilData GetSpoilData(int id)
        {
            if (dicSpoilData.ContainsKey(id))
            {
                return dicSpoilData[id];
            }

            return null;
        }

        public SpoilData[] GetAllSpoilData()
        {
            int index = 0;
            SpoilData[] results = new SpoilData[dicSpoilData.Count];
            foreach (var spoilData in dicSpoilData.Values)
            {
                results[index++] = spoilData;
            }

            return results;
        }

        protected override void OnUnload()
        {
            GameEntry.DataTable.DestroyDataTable<DRSpoil>();
            dtSpoil = null;
            dicSpoilData = null;
        }

        protected override void OnShutdown()
        {
        }
    }

    public class SpoilData : IGridItemData
    {
        private DRSpoil dRSpoil;

        public int ID
        {
            get
            {
                return dRSpoil.Id;
            }
        }
        public string ItemName
        {
            get
            {
                return dRSpoil.Name;
            }
        }
        public int Level
        {
            get
            {
                return dRSpoil.Level;
            }
        }
        public int ImageID
        {
            get
            {
                return dRSpoil.ImageID;
            }
        }

        public string ItemType
        {
            get
            {
                return ((EnumItemType)dRSpoil.Type).ToString();
            }
        }

        public string ItemDescription
        {
            get
            {
                return dRSpoil.Description;
            }
        }
        public int Price
        {
            get
            {
                return dRSpoil.Price;
            }
        }
        public SpoilData(DRSpoil dRSpoil)
        {
            this.dRSpoil = dRSpoil;
        }
    }
}
