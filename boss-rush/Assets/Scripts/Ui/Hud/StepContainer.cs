using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ui.Hud
{
    public class StepContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stepText;
        [SerializeField] private CanvasGroup canvasGroup;

        public void Show(string stepDescription, TweenCallback onComplete)
        {
            stepText.text = stepDescription;
            canvasGroup
                .DOFade(1, 2f)
                .OnComplete(onComplete);
        }

        public void Hide(TweenCallback onComplete)
        {
            canvasGroup
                .DOFade(0, .5f)
                .OnComplete(onComplete);
        }
    }
}