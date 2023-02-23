using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
namespace Fishing
{
    public class UICountDownForm : UGuiFormEx
    {
        [SerializeField]
        private Text NumberText;
        private int m_Number;
        private int Number{
            set{
                m_Number = value;
                NumberText.text = value.ToString();
            }
            get{
                return m_Number;
            }
        }
        private Action m_OnFinishCallback;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            CountDownParams countDownParams = userData as CountDownParams;
            Number = countDownParams.Number;
            m_OnFinishCallback = countDownParams.OnFinishCallback;
            countDownParams.Clear();
            StartCoroutine("CountDown");
        }
        public void Stop(){
            StopCoroutine("CountDown");
        }
        IEnumerator CountDown()
        {
            while(Number>0)
            {
                yield return new WaitForSeconds(1f);
                Number--;
            }
            m_OnFinishCallback.Invoke();
            Close();
        }
    }
}