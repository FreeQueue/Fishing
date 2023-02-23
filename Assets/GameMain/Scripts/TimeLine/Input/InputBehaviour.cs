using System;
using UnityEngine.Playables;
using UnityEngine;
using System.Collections.Generic;
namespace Fishing
{
    [Serializable]
    public class InputBehaviour : PlayableBehaviour
    {
        [SerializeField]
        private List<InputSys.EnumInput> enumInputs;
        public int index;
        public void SetInputEnvironment()
        {
            GameEntry.Input.SetInputEnvironment(enumInputs.ToArray());
        }
    }
}
