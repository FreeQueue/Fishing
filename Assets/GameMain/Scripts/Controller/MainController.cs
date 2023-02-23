using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Fsm;
namespace Fishing
{
    public class MainController : IReference
    {
        private EntityLoader entityLoader;
        private Dictionary<EnumEntity, int> m_EntityIdDic;
        public Timer.DayTimer dayTimer
        {
            get;
            private set;
        }
        public BuffController m_BuffController;
        private IFsm<MainController> m_MainGameFsm;
        public void Start()
        {
            SpawnEntity();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameStart"));
            m_MainGameFsm = GameEntry.Fsm.CreateFsm<MainController>(this, new Normal(), new MainGame());
            m_MainGameFsm.Start<Normal>();
            //dayTimer.StartDayTimer();
        }
        public void Shutdown()
        {
            //dayTimer.ShutdownDayTimer();
            HideAllEntity();
            GameEntry.Fsm.DestroyFsm(m_MainGameFsm);
            m_MainGameFsm = null;
        }
        private void HideAllEntity()
        {
            foreach (var item in m_EntityIdDic)
            {
                entityLoader.HideEntity(item.Value);
            }
            m_EntityIdDic.Clear();
        }
        private void SpawnEntity()
        {
            foreach (var item in GameEntry.PlayerData.GetSceneEntityDatas())
            {
                if (m_EntityIdDic.ContainsKey(item.enumEntity))
                {
                    Log.Error("There is repetitive entity {0} in Scene", item.enumEntity.ToString());
                    continue;
                }
                int i = entityLoader.ShowEntity(item.enumEntity, TypeUtility.GetEntityType(item.enumEntity.ToString()), (entity) =>
                {
                    entity.Logic.Visible = item.IsActive;
                    // Debug.Log(item.IsActive);
                }, EntityData.Create(item.Position, item.Rotation, item.Scale));
                // Debug.Log($"{item.Position},{item.Rotation},{item.Scale}");
                m_EntityIdDic.Add(item.enumEntity, i);
            }
        }

        public Entity GetEntity(EnumEntity enumEntity)
        {
            if (m_EntityIdDic.ContainsKey(enumEntity))
            {
                return entityLoader.GetEntity(m_EntityIdDic[enumEntity]);
            }
            return null;
        }

        public void HideEntity(EnumEntity enumEntity)
        {
            if (m_EntityIdDic.ContainsKey(enumEntity))
            {
                entityLoader.HideEntity(m_EntityIdDic[enumEntity]);
            }
        }
        public void SetEntityVisible(EnumEntity enumEntity, bool value)
        {
            if (m_EntityIdDic.ContainsKey(enumEntity))
            {
                GetEntity(enumEntity).Logic.Visible = value;
            }
            else
            {
                Log.Warning("There is no Entity {0} in the scene", enumEntity.ToString());
            }
        }
        public static MainController Create()
        {
            var mainController = ReferencePool.Acquire<MainController>();
            if (mainController.entityLoader == null) mainController.entityLoader = EntityLoader.Create(mainController);
            if (mainController.m_EntityIdDic == null) mainController.m_EntityIdDic = new Dictionary<EnumEntity, int>();
            if (mainController.dayTimer == null) mainController.dayTimer = new Timer.DayTimer();
            if (mainController.m_BuffController == null) mainController.m_BuffController = new BuffController();
            return mainController;
        }
        public void Clear()
        {
            m_EntityIdDic.Clear();
        }

    }
}