using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fishing
{
    public class UIDayChangeForm : UGuiFormEx
    {
        protected override float FadeTime => 1f;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            StartCoroutine(Wait(1));
            GameEntry.Sound.PlayMusic(EnumSound.昼夜);
        }

        private IEnumerator Wait(float duration)
		{
            yield return new WaitForSeconds(duration);
            Close();
        }
    }
}