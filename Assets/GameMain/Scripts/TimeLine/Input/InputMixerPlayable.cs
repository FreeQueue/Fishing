using UnityEngine;
using UnityEngine.Playables;
namespace Fishing
{
    public class InputMixerPlayable : PlayableBehaviour
    {
        private InputBehaviour activeInput;
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (playable.GetInputWeight(activeInput.index) > 0) return;
            GetActive(playable);

            if (activeInput != null)
            {
                activeInput.SetInputEnvironment();
            }
            else
            {
                GameEntry.Input.ClearInputEnvironment();
            }
        }
        private void GetActive(Playable playable)
        {
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                if (playable.GetInputWeight(i) > 0)
                {
                    activeInput = playable.GetInput(i).GetGraph().GetResolver() as InputBehaviour;
                    activeInput.index = i;
                    return;
                }
            }
        }
    }
}