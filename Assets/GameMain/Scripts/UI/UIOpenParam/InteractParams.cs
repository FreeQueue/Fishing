using UnityEngine;
using GameFramework;
using System;
namespace Fishing
{
    public class InteractParams : IReference
    {
        public string Tips
        {
            get;
            private set;
        }
        public static InteractParams Create(string tips)
        {
            InteractParams interactParams = ReferencePool.Acquire<InteractParams>();
            interactParams.Tips = tips;
            return interactParams;
        }
        public void Clear()
        {
            Tips = null;
        }
    }
}