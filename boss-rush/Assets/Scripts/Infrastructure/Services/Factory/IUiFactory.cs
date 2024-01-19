using Infrastructure.Services.WindowsManager;
using Ui.Windows;

namespace Infrastructure.Services.Factory
{
    public interface IUiFactory
    {
        T CreateWindow<T>(WindowType type, IWindowsManager windowsManager, object[] args) where T : Window;
        void CreateHud();
    }
}