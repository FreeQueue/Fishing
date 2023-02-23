using UnityEngine;
namespace Fishing
{
    public class SceneEntityData
    {
        public EnumEntity enumEntity;
        public bool IsActive;
        public Vector3 Position, Scale;
        public Quaternion Rotation;
        public SceneEntityData(SceneEntityLoader sceneEntityLoader)
        {
            enumEntity = sceneEntityLoader.enumEntity;
            Position = sceneEntityLoader.transform.position;
            Rotation = sceneEntityLoader.transform.rotation;
            Scale = sceneEntityLoader.transform.localScale;
            IsActive = sceneEntityLoader.gameObject.activeSelf;
        }
        public SceneEntityData()
        {

        }
    }
}