//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-06-29 22:30:06.403
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Fishing
{
    /// <summary>
    /// 钓竿配置表。
    /// </summary>
    public class DRRod : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取配置编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取钓竿等级。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取初始进度。
        /// </summary>
        public int Percentage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取捕捉手大小。
        /// </summary>
        public int CatcherScale
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取额外战利品概率。
        /// </summary>
        public int ExtraChance
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取等级1。
        /// </summary>
        public int Level1
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取等级2。
        /// </summary>
        public int Level2
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取等级3。
        /// </summary>
        public int Level3
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取等级4。
        /// </summary>
        public int Level4
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取等级5。
        /// </summary>
        public int Level5
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            Level = int.Parse(columnStrings[index++]);
            Percentage = int.Parse(columnStrings[index++]);
            CatcherScale = int.Parse(columnStrings[index++]);
            ExtraChance = int.Parse(columnStrings[index++]);
            Level1 = int.Parse(columnStrings[index++]);
            Level2 = int.Parse(columnStrings[index++]);
            Level3 = int.Parse(columnStrings[index++]);
            Level4 = int.Parse(columnStrings[index++]);
            Level5 = int.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Level = binaryReader.Read7BitEncodedInt32();
                    Percentage = binaryReader.Read7BitEncodedInt32();
                    CatcherScale = binaryReader.Read7BitEncodedInt32();
                    ExtraChance = binaryReader.Read7BitEncodedInt32();
                    Level1 = binaryReader.Read7BitEncodedInt32();
                    Level2 = binaryReader.Read7BitEncodedInt32();
                    Level3 = binaryReader.Read7BitEncodedInt32();
                    Level4 = binaryReader.Read7BitEncodedInt32();
                    Level5 = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private KeyValuePair<int, int>[] m_Level = null;

        public int LevelCount
        {
            get
            {
                return m_Level.Length;
            }
        }

        public int GetLevel(int id)
        {
            foreach (KeyValuePair<int, int> i in m_Level)
            {
                if (i.Key == id)
                {
                    return i.Value;
                }
            }

            throw new GameFrameworkException(Utility.Text.Format("GetLevel with invalid id '{0}'.", id.ToString()));
        }

        public int GetLevelAt(int index)
        {
            if (index < 0 || index >= m_Level.Length)
            {
                throw new GameFrameworkException(Utility.Text.Format("GetLevelAt with invalid index '{0}'.", index.ToString()));
            }

            return m_Level[index].Value;
        }

        private void GeneratePropertyArray()
        {
            m_Level = new KeyValuePair<int, int>[]
            {
                new KeyValuePair<int, int>(1, Level1),
                new KeyValuePair<int, int>(2, Level2),
                new KeyValuePair<int, int>(3, Level3),
                new KeyValuePair<int, int>(4, Level4),
                new KeyValuePair<int, int>(5, Level5),
            };
        }
    }
}
