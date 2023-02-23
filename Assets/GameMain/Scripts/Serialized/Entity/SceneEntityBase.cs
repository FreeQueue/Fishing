using UnityEngine;
using System.Collections.Generic;
using GameFramework.Event;
using System.IO;
using System.Text;
using UnityGameFramework.Runtime;
using GameFramework;
using System;

namespace Fishing
{
    public class SceneEntityBase
    {

        private SceneEntitySerializer m_Serializer = null;
        private List<SceneEntityData> m_SceneEntityDatas;
        public SceneEntityData[] SceneEntityDatas{
            get{
                return m_SceneEntityDatas.ToArray();
            }
        }
        private string m_FileName=Constant.FilePath.SceneEntity;
        /// <summary>
        /// 获取实体信息文件路径。
        /// </summary>
        public virtual string FilePath
        {
            get
            {
                return Utility.Path.GetRegularPath(Path.Combine(Application.persistentDataPath,m_FileName ));
            }
        }
        public void Reset()
        {
            m_FileName = Constant.FilePath.DefaultSceneEntity;
            Load();
            m_FileName = Constant.FilePath.SceneEntity;
            Save();
        }
        /// <summary>
        /// 加载实体信息。
        /// </summary>
        /// <returns>是否加载实体信息成功。</returns>
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
                Log.Warning($"Load SceneEntity failure with exception '{exception.ToString()}'.");
                return false;
            }
        }

        /// <summary>
        /// 保存实体信息。
        /// </summary>
        /// <returns>是否保存实体信息成功。</returns>
        public bool Save()
        {
            try
            {
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    return m_Serializer.Serialize(fileStream, this);
                }
            }
            catch (Exception exception)
            {
                Log.Warning($"Save SceneEntity failure with exception '{exception.ToString()}'.");
                return false;
            }
        }

        public void Init()
        {
            m_Serializer = new SceneEntitySerializer();
            m_SceneEntityDatas = new List<SceneEntityData>();
            m_Serializer.RegisterSerializeCallback(0, SerializeSceneEntityCallback);
            m_Serializer.RegisterDeserializeCallback(0, DeserializeSceneEntityCallback);
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
                Log.Warning($"Delete SceneEntity failure with exception '{exception.ToString()}'.");
                return false;
            }
        }
        private bool SerializeSceneEntityCallback(Stream stream, SceneEntityBase sceneEntityBase)
        {
            Serialize(stream);
            return true;
        }

        private SceneEntityBase DeserializeSceneEntityCallback(Stream stream)
        {
            Deserialize(stream);
            return this;
        }


        /// <summary>
        /// 序列化数据。
        /// </summary>
        /// <param name="stream">目标流。</param>
        public void Serialize(Stream stream)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(stream, Encoding.UTF8))
            {
                binaryWriter.Write7BitEncodedInt32(m_SceneEntityDatas.Count);
                foreach (var entityData in m_SceneEntityDatas)
                {
                    binaryWriter.Write7BitEncodedInt32(((int)entityData.enumEntity));
                    binaryWriter.Write(entityData.IsActive);
                    binaryWriter.Write(entityData.Position.x);
                    binaryWriter.Write(entityData.Position.y);
                    binaryWriter.Write(entityData.Position.z);
                    binaryWriter.Write(entityData.Rotation.x);
                    binaryWriter.Write(entityData.Rotation.y);
                    binaryWriter.Write(entityData.Rotation.z);
                    binaryWriter.Write(entityData.Rotation.w);
                    binaryWriter.Write(entityData.Scale.x);
                    binaryWriter.Write(entityData.Scale.y);
                    binaryWriter.Write(entityData.Scale.z);
                }
            }
        }

        /// <summary>
        /// 反序列化数据。
        /// </summary>
        /// <param name="stream">指定流。</param>
        public void Deserialize(Stream stream)
        {
            m_SceneEntityDatas.Clear();
            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8))
            {
                int entityCount = binaryReader.Read7BitEncodedInt32();
                for (int i = 0; i < entityCount; i++)
                {
                    SceneEntityData sceneEntityData = new SceneEntityData();
                    sceneEntityData.enumEntity = (EnumEntity)binaryReader.Read7BitEncodedInt32();
                    sceneEntityData.IsActive = binaryReader.ReadBoolean();
                    sceneEntityData.Position = binaryReader.ReadVector3();
                    sceneEntityData.Rotation =binaryReader.ReadQuaternion();
                    sceneEntityData.Scale = binaryReader.ReadVector3();
                    m_SceneEntityDatas.Add(sceneEntityData);
                }
            }
        }
    }
}