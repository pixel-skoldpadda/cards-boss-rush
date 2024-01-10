using System;
using DG.Tweening;
using Infrastructure.Services.WindowsManager;
using UnityEngine;
using Zenject;

namespace Ui.Windows
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private CanvasGroup layout;

        private Tweener _fadeTween;
        
        private Action _onWindowClosed;
        private Action _onWindowOpened;

        protected IWindowsManager WindowsManager;

        [Inject]
        protected void Construct(IWindowsManager windowsManager)
        {
            WindowsManager = windowsManager;
        }

        private void Awake()
        {
            layout.alpha = 0f;
        }

        protected virtual void OnDestroy()
        {
            if (_fadeTween != null)
            {
                _fadeTween.Kill();
                _fadeTween = null;
            }
            _onWindowClosed = null;
            _onWindowOpened = null;
        }

        public void Show()
        {
            Fade(1f, 0.5f, () =>
            {
                layout.interactable = true;
                layout.blocksRaycasts = true;
                _onWindowOpened?.Invoke();
            });
        }

        public void Hide()
        {
            Fade(0f, 0.5f, () =>
            {
                layout.interactable = false;
                layout.blocksRaycasts = false;
                _onWindowClosed?.Invoke();
                Destroy(gameObject);
            });
        }

        protected void Close()
        {
            WindowsManager.CloseWindow(this);
        }

        private void Fade(float endValue, float duration, TweenCallback endCallBack)
        {
            _fadeTween?.Kill();
            _fadeTween = layout.DOFade(endValue, duration);
            _fadeTween.onComplete += endCallBack;
        }

        public Action OnWindowClosed
        {
            get => _onWindowClosed;
            set => _onWindowClosed = value;
        }

        public Action OnWindowOpened
        {
            get => _onWindowOpened;
            set => _onWindowOpened = value;
        }
    }
}