using DG.Tweening;
using UnityEngine;

namespace Ui.Hud.MiddleContainers
{
    public class BaseMiddleContainer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        public void Show(TweenCallback onComplete)
        {
            canvasGroup
                .DOFade(1, 2f)
                .OnComplete(onComplete);
        }
        
        public void Hide(TweenCallback onComplete)
        {
            canvasGroup
                .DOFade(0, 2f)
                .OnComplete(onComplete);
        }
    }
}