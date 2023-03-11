using UnityEngine;
using UnityEngine.UI;

namespace AdvantTest.UI
{
    [RequireComponent(typeof(Button))]
    public sealed class ButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        public bool IsClicked { get; private set; }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            IsClicked = false;
        }

        private void OnButtonClicked()
        {
            IsClicked = true;
        }

        private void LateUpdate()
        {
            IsClicked = false;
        }
    }
}