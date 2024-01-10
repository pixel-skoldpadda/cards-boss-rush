using DG.Tweening;
using UnityEngine;

namespace Ui.Curtain
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup curtain;
        
        private Tween _fadeTwin;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            _fadeTwin?.Kill(true);
            gameObject.SetActive(true);
            curtain.alpha = 1;
        }

        public void Hide()
        {
            _fadeTwin = curtain.DOFade(0f, 1.5f)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    _fadeTwin = null;
                });
        }

        private void OnDestroy()
        {
            if (_fadeTwin != null)
            {
                _fadeTwin.Kill();
                _fadeTwin = null;
            }
        }
    }
}