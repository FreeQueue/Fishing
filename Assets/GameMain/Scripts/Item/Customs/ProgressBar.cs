using UnityEngine;
using UnityEngine.UI;
namespace Fishing
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] public Slider catchProgressBar; //The bar on the right that shows how much you have caught
        public void Init()
        {
            catchProgressBar.maxValue = 100;
        }
        public void UpdatePercentage(float value)
        {
            catchProgressBar.value = value;
        }
    }
}