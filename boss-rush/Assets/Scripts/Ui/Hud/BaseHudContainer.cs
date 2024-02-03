using DG.Tweening;
using UnityEngine;

namespace Ui.Hud
{
    public abstract class BaseHudContainer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Hint hint;
        [SerializeField] [TextArea] private string description;
        
        private void Awake()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;   
            }

            if (hint != null)
            {
                hint.Init(description);
            }
        }

        public virtual void ResetContainer() {}
        
        public void Hide()
        {
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

        public void OnPointerEnter()
        {
            if (hint != null)
            {
                hint.Show();
            }
        }
        
        public void OnPointerExit()
        {
            if (hint != null)
            {
                hint.Hide();
            }
        }
    }
}