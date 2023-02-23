using GameFramework;
using UnityEngine.Events;
using System;
namespace Fishing
{
    public class CountDownParams : IReference
    {
        public int Number
        {
            get;
            private set;
        }
        public Action OnFinishCallback
        {
            get;
            private set;
        }
        public static CountDownParams Create(int number,Action onFinishCallback)
        {
            CountDownParams countDownParams = ReferencePool.Acquire<CountDownParams>();
            countDownParams.Number = number;
            countDownParams.OnFinishCallback = onFinishCallback;
            return countDownParams;
        }
        public void Clear()
        {
            Number = 0;
            OnFinishCallback = null;
        }
    }
}
