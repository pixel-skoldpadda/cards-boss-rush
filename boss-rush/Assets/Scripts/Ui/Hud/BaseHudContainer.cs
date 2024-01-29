using DG.Tweening;
using UnityEngine;

namespace Ui.Hud
{
    public abstract class BaseHudContainer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        protected virtual void ResetContainer() {}
        
        public void Hide()
        {
            ResetContainer();
            canvasGroup
                .DOFade(0, .1f)
                .SetEase(Ease.InExpo);
        }

        public void Show()
        {
            canvasGroup
                .DOFade(1, .1f)
                .SetEase(Ease.InExpo);
        }
    }
}