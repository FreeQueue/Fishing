using UnityEngine;
using System.Collections.Generic;
using GameFramework.Event;
using System.IO;
using System.Text;
using UnityGameFramework.Runtime;
namespace Fishing
{
    public class ItemGridGroupBase
    {
        private readonly Dictionary<int, int> m_ItemDic = new Dictionary<int, int>();
        public bool isCanDrag;
        public int ItemCount
        {
            get
            {
                return m_ItemDic.Count;
            }
        }
        public int GridCount
        {
            get;
            private set;
        }
        public EnumGrid GridGroupType
        {
            get;
        }
        public ItemGridGroupBase(EnumGrid enumGrid, int GridCount)
        {
            this.GridGroupType = enumGrid;
            this.GridCount = GridCount;
        }
        /// <summary>
        /// 物品格中是否有物品
        /// </summary>
        /// <param name="itemID">物品ID</param>
        public bool HasItem(int itemID)
        {
            foreach (var item in m_ItemDic)
            {
                if (item.Value == itemID) return true;
            }
            return false;
        }
        /// <summary>
        /// 从物品组中获得所有物品ID
        /// </summary>
        public int[] GetAllItem()
        {
            int index = 0;
            int[] gridItemIDs = new int[GridCount];
            foreach (var item in m_ItemDic)
            {
                gridItemIDs[index++] = item.Value;
            }
            return gridItemIDs;
        }

        /// <summary>
        /// 根据物品格ID获得物品ID
        /// </summary>
        /// <param name="gridID">物品格ID</param>
        /// <returns>返回物品ID，返回-1为无此物品</returns>
        public int GetItemFromGrid(int gridID)
        {
            if (m_ItemDic.ContainsKey(gridID)) return m_ItemDic[gridID];
            //Log.Warning("This grid has no item");
            return -1;
        }

        /// <summary>
        /// 根据物品ID获取首个物品格ID
        /// </summary>
        /// <param name="itemID">物品ID</param>
        /// <returns>物品格ID</returns>
        public int GetGridID(int itemID)
        {
            foreach (var item in m_ItemDic)
            {
                if (item.Value == itemID) return item.Key;
            }
            //Log.Warning("This gridGroup has no this item");
            return -1;
        }
        /// <summary>
        /// 根据物品ID获取所有物品格ID
        /// </summary>
        /// <param name="itemID">物品ID</param>
        /// <returns>物品格ID</returns>
        public int[] GetAllGridID(int itemID)
        {
            List<int> temp = new List<int>();
            foreach (var item in m_ItemDic)
            {
                if (item.Value == itemID) temp[0] = item.Key;
            }
            return temp.ToArray();
        }
        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="itemID"></param>
        public void AddItem(int itemID)
        {
            int emptyID = GetFirstEmptyGrid();
            if (emptyID != -1 && emptyID < GridCount)
            {
                AddItem(emptyID, itemID);
            }
            else
            {
                Log.Warning("{0} has no space for itemID:{1}", this.GetType(), itemID);
            }
        }
        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="gridID">物品要放置的物品格</param>
        /// <param name="itemID"></param>
        private void AddItem(int gridID, int itemID)
        {
            m_ItemDic.Add(gridID, itemID);
            GameEntry.Event.Fire(this, AddItemEventArgs.Create(itemID, gridID, GridGroupType));
        }
        /// <summary>
        /// 根据物品格ID移除物品
        /// </summary>
        /// <param name="gridID"></param>
        /// <returns></returns>
        public bool RemoveItemFromGrid(int gridID)
        {
            if (!m_ItemDic.ContainsKey(gridID)) Log.Warning("This Grid has no item");
            GameEntry.Event.Fire(this,RemoveItemEventArgs.Create(gridID,GridGroupType));
            return m_ItemDic.Remove(gridID);
        }

        public void RemoveAllItem()
        {
            foreach (var item in m_ItemDic)
            {
                GameEntry.Event.Fire(this,RemoveItemEventArgs.Create(item.Key,GridGroupType));
            }
            m_ItemDic.Clear();
        }

        public int GetFirstEmptyGrid()
        {
            for (int i = 0; i < GridCount; i++)
            {
                if (!m_ItemDic.ContainsKey(i)) return i;
            }
            return -1;
        }
        /// <summary>
        /// 序列化数据。
        /// </summary>
        /// <param name="stream">目标流。</param>
        public void Serialize(Stream stream)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8))
            {
                binaryWriter.Write7BitEncodedInt32(ItemCount);
                foreach (var gridItem in m_ItemDic)
                {
                    binaryWriter.Write7BitEncodedInt32(gridItem.Key);
                    binaryWriter.Write7BitEncodedInt32(gridItem.Value);
                }
            }
        }

        /// <summary>
        /// 反序列化数据。
        /// </summary>
        /// <param name="stream">指定流。</param>
        public void Deserialize(Stream stream)
        {
            m_ItemDic.Clear();
            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8))
            {
                int itemCount = binaryReader.Read7BitEncodedInt32();
                for (int i = 0; i < itemCount; i++)
                {
                    int id = binaryReader.Read7BitEncodedInt32();
                    int itemID = binaryReader.Read7BitEncodedInt32();
                    m_ItemDic.Add(id, itemID);
                }
            }
        }
    }
}