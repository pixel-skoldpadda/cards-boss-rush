using Infrastructure.Services.Items;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States;
using Items;
using Ui.Windows;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class UiFactory : IUiFactory
    {
        private readonly IItemsService _items;
        private readonly DiContainer _diContainer;
        private readonly IGameStateMachine _stateMachine;

        [Inject]
        public UiFactory(IItemsService items, DiContainer diContainer)
        {
            _items = items;
            _diContainer = diContainer;
        }
        
        public T CreateWindow<T>(WindowType type, IWindowsManager windowsManager, object[] args) where T : Window
        {
            WindowItem item = _items.GetWindowItem(type);
            Window window = _diContainer.InstantiatePrefabForComponent<T>(item.Prefab);

            return (T) window;
        }
    }
}