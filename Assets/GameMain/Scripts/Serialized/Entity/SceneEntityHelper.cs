using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityGameFramework.Runtime;
using GameFramework;
using System;
namespace Fishing
{
    public class SceneEntityHelper : MonoBehaviour
    {
        private SceneEntitySerializer m_SceneEntitySerializer;
        public string FilePath
        {
            get
            {
                return Utility.Path.GetRegularPath(Path.Combine(Application.persistentDataPath, Constant.FilePath.DefaultSceneEntity));
            }
        }
        List<SceneEntityData> m_SceneEntityDatas;
        [ContextMenu("Save entity tool")]
        public void SaveEntityTool()
        {
            m_SceneEntityDatas = new List<SceneEntityData>();
            SceneEntityLoader[] sceneEntityLoaders = transform.GetComponentsInChildren<SceneEntityLoader>();
            m_SceneEntitySerializer = new SceneEntitySerializer();
            m_SceneEntitySerializer.RegisterSerializeCallback(0, SerializeDefaultSceneEntityCallback);
            foreach (var item in sceneEntityLoaders)
            {
                m_SceneEntityDatas.Add(new SceneEntityData(item));
            }
            Save();
        }
        
        private bool SerializeDefaultSceneEntityCallback(Stream stream, SceneEntityBase sceneEntityBase)
        {
            Serialize(stream);
            return true;
        }
        public void Save()
        {
            try
            {
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    m_SceneEntitySerializer.Serialize(fileStream, null);
                }
            }
            catch (Exception exception)
            {
                Log.Warning($"Save SceneEntity failure with exception '{exception.ToString()}'.");
            }
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
    }
}