using System;
using Ui.Windows;

namespace Infrastructure.Services.WindowsManager
{
    public class WindowData
    {
        public WindowType Type { get; }
        public Action OnClose { get; }
        public object[] Args { get; }

        public WindowData(WindowType type, Action onClose, object[] args)
        {
            Type = type;
            OnClose = onClose;
            Args = args;
        }
    }
}