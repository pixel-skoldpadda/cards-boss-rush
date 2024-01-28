using Ui.Windows;

namespace Infrastructure.Services.Factory
{
    public interface IUiFactory
    {
        T CreateWindow<T>(WindowType type, object[] args) where T : Window;
    }
}