using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Fishing
{
    [RequireComponent(typeof(Button))]
    public class MainMenuButton : ItemLogicEx
    {
        [SerializeField]
        private Text text;
        private Button button;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            button = GetComponent<Button>();
        }
        public void AddListener(UnityAction callback)
        {
            button.onClick.AddListener(callback);
            button.onClick.AddListener(PlayUIMusic);
        }
        private void PlayUIMusic()
        {
            GameEntry.Sound.PlayMusic(EnumSound.菜单2);
        }
        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}