using TMPro;
using UnityEngine;

namespace Ui.Hud
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hint;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup.alpha = 0;
        }

        public void Init(string text)
        {
            hint.text = text;
        }

        public void Show()
        {
            canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
        }
    }
}