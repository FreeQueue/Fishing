//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-06-29 22:30:06.382
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
    /// 对话单元配置表。
    /// </summary>
    public class DRDialogUnit : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取对话编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取人物编号。
        /// </summary>
        public int CharacterID
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取在左边。
        /// </summary>
        public bool IsLeft
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取对话。
        /// </summary>
        public string Dialog
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取声音编号。
        /// </summary>
        public int VoiceID
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
            CharacterID = int.Parse(columnStrings[index++]);
            IsLeft = bool.Parse(columnStrings[index++]);
            Dialog = columnStrings[index++];
            VoiceID = int.Parse(columnStrings[index++]);

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
                    CharacterID = binaryReader.Read7BitEncodedInt32();
                    IsLeft = binaryReader.ReadBoolean();
                    Dialog = binaryReader.ReadString();
                    VoiceID = binaryReader.Read7BitEncodedInt32();
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
