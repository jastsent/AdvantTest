using UnityEngine;
using UnityEngine.UI;

namespace AdvantTest.Utils
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public sealed class SpeedButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (UltraTime.DeltaTimeMultiplier > 1f)
            {
                UltraTime.DeltaTimeMultiplier = 1f;
                _image.color = _button.colors.normalColor;
            }
            else
            {
                UltraTime.DeltaTimeMultiplier = 5f;
                _image.color = _button.colors.pressedColor;
            }
        }
    }
}
