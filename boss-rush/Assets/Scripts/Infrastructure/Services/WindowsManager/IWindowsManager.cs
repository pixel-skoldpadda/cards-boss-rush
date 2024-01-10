using System;
using Ui.Windows;

namespace Infrastructure.Services.WindowsManager
{
    public interface IWindowsManager
    {
        void CloseWindow(Window window);
        void OpenWindow(WindowType windowType, bool closeCurrent = false, Action onClose = null, params object[] args);
        Action OnWindowOpened { get; set; }
        Action OnWindowClosed { get; set; }
    }
}