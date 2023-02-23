using GameFramework;
using UnityGameFramework.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Fishing
{
    /// <summary>
    /// 物品格组辅助器。
    /// </summary>
    public class ItemGridGroupHelper : MonoBehaviour
    {
        public string ItemGridGroupName
        {
            get { return EnumType.ToString(); }
        }
        [SerializeField]
        private EnumGrid m_EnumType;
        public EnumGrid EnumType
        {
            get{
                return m_EnumType;
            }
        }
        [SerializeField]
        private int m_ItemGridCount;
        public int ItemGridCount
        {
            get{
                return m_ItemGridCount;
            }
        }
        public ItemGridGroupBase m_ItemGridGroupBase = null;
        private ItemGridGroupSerializer m_Serializer = null;

        /// <summary>
        /// 获取物品格组文件路径。
        /// </summary>
        public virtual string FilePath
        {
            get
            {
                return Utility.Path.GetRegularPath(Path.Combine(Application.persistentDataPath, ItemGridGroupName+".dat"));
            }
        }

        /// <summary>
        /// 获取物品数量。
        /// </summary>
        public int ItemCount
        {
            get
            {
                return m_ItemGridGroupBase != null ? m_ItemGridGroupBase.ItemCount : -1;
            }
        }

        /// <summary>
        /// 加载物品格组。
        /// </summary>
        /// <returns>是否加载物品格组成功。</returns>
        public bool Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return true;
                }
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    m_Serializer.Deserialize(fileStream);
                    return true;
                }
            }
            catch (Exception exception)
            {
                Log.Warning($"Load ItemGrid {ItemGridGroupName} failure with exception '{exception.ToString()}'.");
                return false;
            }
        }

        /// <summary>
        /// 保存物品格组。
        /// </summary>
        /// <returns>是否保存物品格组成功。</returns>
        public bool Save()
        {
            try
            {
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    return m_Serializer.Serialize(fileStream, m_ItemGridGroupBase);
                }
            }
            catch (Exception exception)
            {
                Log.Warning($"Save ItemGrid {ItemGridGroupName} failure with exception '{exception.ToString()}'.");
                return false;
            }
        }

        public void Init()
        {
            m_ItemGridGroupBase = new ItemGridGroupBase(EnumType,m_ItemGridCount);
            m_Serializer = new ItemGridGroupSerializer();
            m_Serializer.RegisterSerializeCallback(0, SerializeDefaultSettingCallback);
            m_Serializer.RegisterDeserializeCallback(0, DeserializeDefaultSettingCallback);
        }
        public bool RemoveFile()
        {
            try{
                if (!File.Exists(FilePath))
                {
                    return true;
                }
                File.Delete(FilePath);
                return true;
            }
            catch (Exception exception)
            {
                Log.Warning($"Delete {ItemGridGroupName} failure with exception '{exception.ToString()}'.");
                return false;
            }
        }
        private bool SerializeDefaultSettingCallback(Stream stream, ItemGridGroupBase itemGridGroupBase)
        {
            m_ItemGridGroupBase.Serialize(stream);
            return true;
        }

        private ItemGridGroupBase DeserializeDefaultSettingCallback(Stream stream)
        {
            m_ItemGridGroupBase.Deserialize(stream);
            return m_ItemGridGroupBase;
        }
    }
}
