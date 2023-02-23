using UnityEngine;
using System.Collections.Generic;
using Fishing.Data;
using GameFramework.Event;
namespace Fishing
{
    public class FishingController
    {
        RodData m_RodData;
        int? uiIndex;
        public void Start()
        {
            DataRod dataRod = GameEntry.Data.GetData<DataRod>();
            m_RodData = dataRod.GetRodDataByLevel(GameEntry.PlayerData.GetData(EnumIntData.RodLevel));
            uiIndex= GameEntry.UI.OpenUIForm(EnumUIForm.UIFishingForm, FishingParams.Create(m_RodData.CatcherScale, 1, m_RodData.Percentage + GameEntry.PlayerData.GetData(EnumIntData.Progress), 15, 10, (1, 5), 5));
            GameEntry.Event.Subscribe(CatchFishEventArgs.EventId, OnCatchFish);
            GameEntry.Event.Subscribe(CatchNothingEventArgs.EventId, OnCatchNothing);
        }
        public void Shutdown()
        {
            GameEntry.UI.CloseUIForm((int)uiIndex);
            GameEntry.Event.Unsubscribe(CatchFishEventArgs.EventId, OnCatchFish);
            GameEntry.Event.Unsubscribe(CatchNothingEventArgs.EventId, OnCatchNothing);
        }
        private void OnCatchNothing(object sender, GameEventArgs e)
        {
            GameEntry.Sound.PlayMusic(EnumSound.失败);
            GameEntry.UI.OpenTipsPopForm("什么也没抓到……");
            GameEntry.Event.Fire(this, FishingFinishEventArgs.Create());
        }
        private void OnCatchFish(object sender, GameEventArgs e)
        {
            List<int> spoilIDs = new List<int>();
            spoilIDs.Add(GetSpoil().ID);
            int rule = Random.Range(0, 100) - m_RodData.ExtraChance;
            if (rule < 0)
            {
                spoilIDs.Add(GetSpoil().ID) ;
            }
            foreach (var spoilID in spoilIDs)
            {
                GameEntry.ItemGrid.GetItemGridGroupHelper(EnumGrid.Bag).m_ItemGridGroupBase.AddItem(spoilID);
            }
            GameEntry.UI.OpenUIForm(EnumUIForm.UIRewardForm,spoilIDs.ToArray());
            GameEntry.Event.Fire(this, FishingFinishEventArgs.Create());
        }
        private SpoilData GetSpoil()
        {
            int level = m_RodData.GetRandomSpoilLevel();
            DataSpoil dataSpoil = GameEntry.Data.GetData<DataSpoil>();
            int rule = Random.Range(0, 10);
            if (rule < 8)
            {
                return dataSpoil.GetRandomMineral(level);
            }
            else
            {
                return dataSpoil.GetRelicOrFortify(level);
            }
        }
    }
}