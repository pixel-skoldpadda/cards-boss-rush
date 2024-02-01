using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ui.Hud.MiddleContainers
{
    public class StepContainer : BaseMiddleContainer
    {
        [SerializeField] private TextMeshProUGUI stepText;

        public void ShowAndHide(string stepDescription, TweenCallback onComplete)
        {
            stepText.text = stepDescription;
            
            Show(() => Hide(onComplete));
        }
    }
}