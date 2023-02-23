using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameFramework.Event;
namespace Fishing
{
    public class UIFishingForm : UGuiFormEx
    {
        [SerializeField]
        private RectTransform m_Center, m_Leader, m_RockRoot;
        [SerializeField]
        private RectTransform cloud1, cloud2, cloud3;
        [SerializeField]
        private ProgressBar m_ProgressBar;
        [SerializeField]
        private float m_Radius;
        private bool m_PauseFlag = false;
        public bool IsWork
        {
            get;
            private set;
        }
        private float Percentage
        {
            get
            {
                return m_Percentage;
            }
            set
            {
                m_Percentage = value;
                if (m_Percentage >= 100)
                {
                    GameEntry.Event.Fire(this, CatchFishEventArgs.Create());
                }
                m_ProgressBar.UpdatePercentage(value);
            }
        }
        private RectTransform m_Fish;
        private Vector3 m_Target, velocity = Vector3.zero;
        private float m_Timer, m_OriginalTime = 10f, m_Percentage, m_RockSpawnSpeed, m_Damage = 0.33f, m_MoveStep = 1f;
        private int m_RockMaxNum;
        private int? m_UICountDown;
        private (int, int) m_RockRange;
        private List<Rock> m_Rocks;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Rocks = new List<Rock>();
            m_ProgressBar.Init();
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            GameEntry.Sound.PlayMusic(EnumSound.水滴2);
            // GameEntry.Sound.PlayMusic(EnumSound.钓鱼);
            IsWork = false;
            Register(InputSys.EnumInput.Cancel, OnCancelButtonDown);
            FishingParams fishingParams = userData as FishingParams;
            m_RockMaxNum = fishingParams.RockMaxNum;
            m_RockRange = fishingParams.RockRange;
            m_RockSpawnSpeed = fishingParams.RockSpawnSpeed;
            Percentage = fishingParams.InitialPercentage;
            m_MoveStep = fishingParams.FishSpeed;
            ShowItem<Fish>(EnumItem.Fish, (item) =>
            {
                m_Fish = item.transform as RectTransform;
                m_Fish.SetParent(m_Center);
                m_Fish.localPosition = Random.insideUnitCircle * m_Radius;
            }, FishParams.Create(OnCatchFish));
            ShowItem<Catcher>(EnumItem.Catcher, (item) =>
            {
                item.transform.SetParent(m_Center);
                item.transform.localPosition = Vector3.zero;
            }, CatcherParams.Create(fishingParams.CatcherScale, m_Leader, fishingParams.CatcherSpeed));
            m_Leader.SetParent(m_Center);
            m_Timer = m_OriginalTime;
            m_UICountDown = GameEntry.UI.OpenCountDownForm(3, OnCountDownFinish);
            Subscribe(RockDestroyEventArgs.EventId, RockDestroy);
            fishingParams.Clear();
            GameEntry.PlayerData.ChangeData(EnumIntData.Strength, -1);
            StartCoroutine("CloudMove");
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (IsWork)
            {
                LeaderMove();
                FishMove();
                MakeRock();
            }
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            var uIForm = GameEntry.UI.GetUIForm((int)m_UICountDown);
            if (uIForm != null)
            {
                var countDownForm = uIForm.Logic as UICountDownForm;
                if (countDownForm != null)
                {
                    countDownForm.Stop();
                }
            }
            IsWork = false;
        }
        private IEnumerator CloudMove()
        {
            float time = 60;
            while (true)
            {
                cloud1.LeanRotateZ(360 * 4, time);
                cloud2.LeanRotateZ(360 * 3, time);
                cloud3.LeanRotateZ(360 * 2, time);
                yield return new WaitForSeconds(time);
            }
        }
        private void OnCancelButtonDown()
        {
            GameEntry.UI.OpenConfirmForm("中途退出仍然会消耗体力，确定退出吗？", () =>
            {
                GameEntry.Event.Fire(this, CatchNothingEventArgs.Create());
                Close();
            }, null);
        }
        protected override void OnPause()
        {
            base.OnPause();
            IsWork = false;
            m_PauseFlag = true;
        }
        protected override void OnResume()
        {
            base.OnResume();
            if (m_PauseFlag) IsWork = true;
        }
        private void RockDestroy(object sender, GameEventArgs e)
        {
            Rock rock = sender as Rock;

            m_Rocks.Remove(rock);
            HideItem(rock.Item);
        }
        private void OnCatchFish()
        {
            if (IsWork) Percentage += m_Damage;
        }
        private void OnCountDownFinish()
        {
            IsWork = true;
        }

        private void ProgressDown()
        {
            Percentage -= 0.01f;
            Debug.Log(Percentage);
        }
        private void LeaderMove()
        {
            Vector2 mousePoint = Input.mousePosition.ToVector2Z() - new Vector2(Screen.width, Screen.height) / 2;
            Vector2 dis = mousePoint.normalized * m_Radius;
            m_Leader.SetLocalPositionX(dis.x);
            m_Leader.SetLocalPositionY(dis.y);
            m_Leader.rotation.eulerAngles.Set(0, 0, Mathf.Atan2(dis.x, dis.y));
        }
        private void FishMove()
        {
            if (Vector3.Distance(m_Fish.localPosition, m_Target) < 1)
                RandomTarget();
            m_Fish.localPosition = Vector3.SmoothDamp(m_Fish.localPosition, m_Target, ref velocity, m_MoveStep*0.1f);
        }
        private void RandomTarget()
        {
            m_Target = (UnityEngine.Random.insideUnitCircle * m_Radius).ToVector3Z();
        }
        private void MakeRock()
        {
            m_Timer -= m_RockSpawnSpeed * Time.fixedDeltaTime;
            //Debug.Log(m_Timer + "    " + Time.deltaTime+"   "+m_RockMaxNum+"   "+m_Rocks.Count);
            if (m_Timer < 0 && m_Rocks.Count < m_RockMaxNum)
            {
                m_Timer = m_OriginalTime;
                ShowItem<Rock>(EnumItem.Rock, (item) =>
                {
                    m_Rocks.Add(item.Logic as Rock);
                    item.transform.SetParent(m_RockRoot);
                    item.transform.localPosition = (Random.insideUnitCircle * m_Radius).ToVector3Z();
                }, Random.Range(m_RockRange.Item1, m_RockRange.Item2));
            }
        }

    }
}