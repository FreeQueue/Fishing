using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
namespace Fishing
{
    public class UIMainMenuForm : UGuiFormEx
    {
        public RectTransform ButtonListRoot;
        private Button startButton,exitButton;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            RefreshButtonList();
        }
        private void RefreshButtonList()
        {
            if(!GameEntry.PlayerData.GetData(EnumBoolData.IsNewGame))
            {
                CreateButton("继续游戏",OnStartButtonClick);
                CreateButton("开始新游戏",OnNewGameButtonClick);
                CreateButton("退出游戏",OnExitButtonClick);
            }
            else
            { 
                CreateButton("开始游戏",OnStartButtonClick);
                CreateButton("退出游戏",OnExitButtonClick);
            }
            
        }
        private void CreateButton(string buttonText,UnityAction callback)
        {
            ShowItem<MainMenuButton>(EnumItem.MainMenuButton, item =>
            {
                item.transform.SetParent(ButtonListRoot);
                MainMenuButton mainMenuButton = item.Logic as MainMenuButton;
                mainMenuButton.AddListener(callback);
                mainMenuButton.SetText(buttonText);
            });
        }
        private void OnStartButtonClick()
        {
            GameEntry.Event.Fire(this, ChangeSceneEventArgs.Create(GameEntry.Config.GetInt("Scene.MainGame")));
        }
        private void OnNewGameButtonClick()
        {
            GameEntry.UI.OpenConfirmForm("这将清除之前的存档，确认要这么做吗？",()=>{
                GameEntry.PlayerData.ResetAllData();
                GameEntry.ItemGrid.Clear();
                OnStartButtonClick();
            },null);
        }
        private void OnExitButtonClick()
        {
            GameEntry.UI.OpenConfirmForm("确认要退出游戏吗？",()=>{
                UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
            },null);
        }
    }
}