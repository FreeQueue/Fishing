using System;
using UnityEngine.Playables;
using UnityEngine;
using System.Collections.Generic;
namespace Fishing
{
    [Serializable]
    public class PlayerControlBehaviour : PlayableBehaviour
    {
        [HideInInspector]
        public int index;
    }
}
