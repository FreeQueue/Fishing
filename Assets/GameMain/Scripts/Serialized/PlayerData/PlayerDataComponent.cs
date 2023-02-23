using UnityEngine;
using GameFramework.Config;
using GameFramework;
using UnityGameFramework.Runtime;
using Fishing.Timer;
namespace Fishing
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/PlayerData")]
    public class PlayerDataComponent : GameFrameworkComponent
    {
        [SerializeField]
        private ItemImage m_RockImage;
        public ItemImage RockImage
        {
            get => m_RockImage;
        }
        public bool IsNeedSubmit
        {
            get
            {
                return GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.ItemCount > 0;
            }
        }
        public bool IsNeedUpgrade
        {
            get
            {
                return GetData(EnumIntData.RodLevel) < GetData(EnumIntData.FortifyNum) / 2;
            }
        }
        private bool flag_Tutorial;
        private PlayerIntDataList playerIntDataList;
        private PlayerBoolDataList playerBoolDataList;
        private SceneEntityBase sceneEntityBase;
        protected override void Awake()
        {
            base.Awake();
            playerIntDataList = new PlayerIntDataList();
            playerBoolDataList = new PlayerBoolDataList();
            sceneEntityBase = new SceneEntityBase();
            sceneEntityBase.Init();
            sceneEntityBase.Load();
        }
        public void ResetAllData()
        {
            playerIntDataList.Reset();
            playerBoolDataList.Reset();
            sceneEntityBase.Reset();
        }
        public SceneEntityData[] GetSceneEntityDatas()
        {
            return sceneEntityBase.SceneEntityDatas;
        }
        public int GetData(EnumIntData dataName)
        {
            return playerIntDataList.GetData(dataName);
        }
        public bool IsDataDefault(EnumIntData dataName)
        {
            return playerIntDataList.IsDataDefault(dataName);
        }
        public int GetDataDefault(EnumIntData dataName)
        {
            return playerIntDataList.GetDataDefault(dataName);
        }
        public void ResetData(EnumIntData dataName)
        {
            playerIntDataList.ResetData(dataName);
        }
        public void SetData(EnumIntData dataName, int value)
        {
            playerIntDataList.SetData(dataName, value);
        }
        public void ChangeData(EnumIntData dataName, int value)
        {
            playerIntDataList.ChangeData(dataName, value);
        }
        public bool GetData(EnumBoolData dataName)
        {
            return playerBoolDataList.GetData(dataName);
        }
        public void SetData(EnumBoolData dataName, bool value)
        {
            playerBoolDataList.SetData(dataName, value);
        }
        private void OnApplicationQuit()
        {
            GameEntry.Setting.Save();
        }
    }
}