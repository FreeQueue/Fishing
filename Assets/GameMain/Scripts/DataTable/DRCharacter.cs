//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-06-29 22:30:06.377
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
    /// 人物配置表。
    /// </summary>
    public class DRCharacter : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取人物编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取人物代号。
        /// </summary>
        public string CharacterName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取人物名牌。
        /// </summary>
        public string NameTag
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取默认说话声音Id。
        /// </summary>
        public int SpeakVoiceIndex
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
            CharacterName = columnStrings[index++];
            NameTag = columnStrings[index++];
            SpeakVoiceIndex = int.Parse(columnStrings[index++]);

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
                    CharacterName = binaryReader.ReadString();
                    NameTag = binaryReader.ReadString();
                    SpeakVoiceIndex = binaryReader.Read7BitEncodedInt32();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
