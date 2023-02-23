using GameFramework;
using UnityEngine.Events;
using System;
namespace Fishing
{
    public class FishingParams : IReference
    {
        public float CatcherScale
        {
            get;
            private set;
        }
        public float CatcherSpeed
        {
            get;
            private set;
        }
        public float InitialPercentage
        {
            get;
            private set;
        }
        public float FishSpeed
        {
            get;
            private set;
        }
        public int RockMaxNum
        {
            get;
            private set;
        }
        public (int, int) RockRange
        {
            get;
            private set;
        }
        public float RockSpawnSpeed
        {
            get;
            private set;
        }
        public static FishingParams Create(float catcherScale,float catcherSpeed, float initialPercentage, float fishSpeed, int rockMaxNum, (int, int) rockRange, float RockSpawnSpeed)
        {
            FishingParams fishingParams = ReferencePool.Acquire<FishingParams>();
            fishingParams.CatcherScale = catcherScale;
            fishingParams.CatcherSpeed = catcherSpeed;
            fishingParams.InitialPercentage = initialPercentage;
            fishingParams.FishSpeed = fishSpeed;
            fishingParams.RockMaxNum = rockMaxNum;
            fishingParams.RockRange = rockRange;
            fishingParams.RockSpawnSpeed = RockSpawnSpeed;
            return fishingParams;
        }
        public void Clear()
        {

        }
    }
}
