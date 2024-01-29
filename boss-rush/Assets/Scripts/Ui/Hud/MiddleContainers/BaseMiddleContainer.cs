using DG.Tweening;
using UnityEngine;

namespace Ui.Hud.MiddleContainers
{
    public class BaseMiddleContainer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private Tweener _fadeZero;
        private Tweener _fadeOne;
        
        public void Show(TweenCallback onComplete)
        {
            _fadeZero = canvasGroup
                .DOFade(1, 1f)
                .OnComplete(onComplete);
        }
        
        public void Hide(TweenCallback onComplete = null)
        {
            _fadeOne = canvasGroup
                .DOFade(0, 1f)
                .OnComplete(onComplete);
        }

        private void OnDestroy()
        {
            _fadeOne?.Kill();
            _fadeOne = null;
            
            _fadeZero.Kill();
            _fadeZero = null;
        }
    }
}