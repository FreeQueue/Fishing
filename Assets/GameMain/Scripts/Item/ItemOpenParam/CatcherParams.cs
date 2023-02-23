using UnityEngine;
using GameFramework;
namespace Fishing
{
    public class CatcherParams : IReference
    {
        public float CatcherScale
        {
            get;
            private set;
        }
        public Transform Target
        {
            get;
            private set;
        }
        public float Speed
        {
            get;
            private set;
        }
        public static CatcherParams Create(float catcherScale,Transform target, float speed)
        {
            CatcherParams catcherParams = ReferencePool.Acquire<CatcherParams>();
            catcherParams.CatcherScale = catcherScale;
            catcherParams.Target = target;
            catcherParams.Speed = speed;
            return catcherParams;
        }
        public void Clear()
        {

        }
    }
}