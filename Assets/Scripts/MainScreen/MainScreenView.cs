using TMPro;
using UnityEngine;

namespace AdvantTest.MainScreen
{
    public sealed class MainScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI balanceText;
        [field:SerializeField] public RectTransform ContentTransform { get; private set; }

        public void SeBalance(string str)
        {
            balanceText.text = str;
        }
    }
}
