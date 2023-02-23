using UnityEngine;
using System;
using GameFramework;
namespace Fishing
{
    public class FishParams : IReference
    {
        public Action CatchCallback
        {
            get;
            private set;
        }
        public static FishParams Create(Action catchCallback)
        {
            FishParams fishParams = ReferencePool.Acquire<FishParams>();
            fishParams.CatchCallback = catchCallback;
            return fishParams;
        }
        public void Clear()
        {
            CatchCallback = null;
        }
    }
}