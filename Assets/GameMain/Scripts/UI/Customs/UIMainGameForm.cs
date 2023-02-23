using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameFramework.Event;
namespace Fishing
{
    public class UIMainGameForm : UGuiFormEx
    {
        [SerializeField]
        private Button bagButton, helperButton;
        [SerializeField]
        private Text Strength, Money, DayState;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            bagButton.onClick.AddListener(OnBagButtonClick);
            helperButton.onClick.AddListener(OnHelperButtonClick);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Register(InputSys.EnumInput.Bag, OnBagButtonClick);
            Register(InputSys.EnumInput.Helper, OnHelperButtonClick);
            Register(InputSys.EnumInput.Cancel, OnBackButtonClick);
            Subscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.Money), OnMoneyChange);
            Subscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.Strength), OnStrengthChange);
            Subscribe(PlayerDataChangeEventArgs.EventId(EnumIntData.DayState), OnDayStateChange);
            Money.text = GameEntry.PlayerData.GetData(EnumIntData.Money).ToString();
            Strength.text = GameEntry.PlayerData.GetData(EnumIntData.Strength).ToString();
            DayState.text = ((EnumDayState)GameEntry.PlayerData.GetData(EnumIntData.DayState)).ToString();
        }
        protected override void OnPause()
        {
            base.OnPause();
            foreach (var item in GameEntry.UI.GetUIGroup("MainGameInfo").GetAllUIForms())
            {
                item.OnPause();
            }
            
            GameEntry.Input.Register(InputSys.EnumInput.Interact, EmptyAction);
        }
        protected override void OnResume()
        {
            base.OnResume();
            foreach (var item in GameEntry.UI.GetUIGroup("MainGameInfo").GetAllUIForms())
            {
                item.OnResume();
            }
            GameEntry.Input.Unregister(InputSys.EnumInput.Interact, EmptyAction);
        }
        private void EmptyAction() { }
        private void OnBagButtonClick()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UIBagForm);
        }
        private void OnHelperButtonClick()
        {
            GameEntry.UI.OpenUIForm(EnumUIForm.UIHelperForm);
        }
        private void OnBackButtonClick()
        {
            GameEntry.UI.OpenConfirmForm("确定要回到标题画面吗？", () =>
            {
                GameEntry.Event.Fire(this, ChangeSceneEventArgs.Create(GameEntry.Config.GetInt("Scene.Menu")));
            }, null);
        }
        private void OnMoneyChange(object sender, GameEventArgs e)
        {
            PlayerDataChangeEventArgs ne = e as PlayerDataChangeEventArgs;
            Money.text = ((int)ne.Data).ToString();
        }
        private void OnStrengthChange(object sender, GameEventArgs e)
        {
            PlayerDataChangeEventArgs ne = e as PlayerDataChangeEventArgs;
            Strength.text = ((int)ne.Data).ToString();
        }
        private void OnDayStateChange(object sender, GameEventArgs e)
        {
            PlayerDataChangeEventArgs ne = e as PlayerDataChangeEventArgs;
            DayState.text = ((EnumDayState)ne.Data).ToString();
        }
    }
}