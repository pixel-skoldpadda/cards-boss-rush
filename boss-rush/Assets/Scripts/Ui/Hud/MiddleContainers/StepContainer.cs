using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ui.Hud.MiddleContainers
{
    public class StepContainer : BaseMiddleContainer
    {
        [SerializeField] private TextMeshProUGUI stepText;

        public void Show(string stepDescription, TweenCallback onComplete)
        {
            stepText.text = stepDescription;
            
            base.Show(onComplete);
        }
    }
}