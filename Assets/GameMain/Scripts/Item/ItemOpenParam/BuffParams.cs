using UnityEngine;
using GameFramework;
namespace Fishing
{
    public class BuffParams:IReference
    {
        public int ImageID{
            get;
            private set;
        }
        public string Name{
            get;
            private set;
        }
        public string Description{
            get;
            private set;
        }
        public static BuffParams Create(int imageID,string name,string description)
        {
            BuffParams buffParams = ReferencePool.Acquire<BuffParams>();
            buffParams.ImageID = imageID;
            buffParams.Name = name;
            buffParams.Description = description;
            return buffParams;
        }
        public void Clear()
        {

        }
    }
}