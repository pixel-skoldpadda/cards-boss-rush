using System;
using System.Collections.Generic;
using Infrastructure.Services.Factory;
using Ui.Windows;
using Zenject;

namespace Infrastructure.Services.WindowsManager
{
    public class WindowsManager : IWindowsManager
    {
        private readonly IUiFactory _uiFactory;
        
        private readonly Queue<WindowData> _queue = new();
        private Window _current;

        private Action _onWindowOpened;
        private Action _onWindowClosed;

        [Inject]
        public WindowsManager(IUiFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public void CloseWindow(Window window)
        {
            window.OnWindowClosed += () =>
            {
                _current = null;
                _onWindowClosed?.Invoke();
                TryOpenNextWindow();
            };
            window.Hide();
        }

        public void OpenWindow(WindowType windowType, bool closeCurrent = false, Action onClose = null, params object[] args)
        {
            if (closeCurrent && _current != null)
            {
                _queue.Enqueue(new WindowData(windowType, onClose, args));
                CloseWindow(_current);
                return;
            }

            if (_current == null)
            {
                Window window = _uiFactory.CreateWindow<Window>(windowType, this, args);
                if (onClose != null)
                {
                    window.OnWindowClosed += onClose;   
                }
                
                _current = window;
                window.Show();
                _onWindowOpened?.Invoke();
            }
            else
            {
                _queue.Enqueue(new WindowData(windowType, onClose, args));   
            }
        }

        private void TryOpenNextWindow()
        {
            if (_queue.TryDequeue(out var windowData))
            {
                OpenWindow(windowData.Type, false, windowData.OnClose, windowData.Args);
            }
        }

        public Action OnWindowOpened
        {
            get => _onWindowOpened;
            set => _onWindowOpened = value;
        }

        public Action OnWindowClosed
        {
            get => _onWindowClosed;
            set => _onWindowClosed = value;
        }
    }
}