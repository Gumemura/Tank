using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class CircleFillHandler : MonoBehaviour
    {
        private Image circleFillImage;

        // Start is called before the first frame update
        void Start()
        {
            circleFillImage = GetComponent<Image>();
        }

        public void FillCircleValue(float value)
        {
            float fillAmount = (value);
            circleFillImage.fillAmount = fillAmount;
        }
    }
}