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
        private readonly IInstantiator _instantiator;
        private readonly IGameStateMachine _stateMachine;

        [Inject]
        public UiFactory(IItemsService items, IInstantiator instantiator)
        {
            _items = items;
            _instantiator = instantiator;
        }
        
        public T CreateWindow<T>(WindowType type, IWindowsManager windowsManager, object[] args) where T : Window
        {
            WindowItem item = _items.GetWindowItem(type);
            Window window = _instantiator.InstantiatePrefabForComponent<T>(item.Prefab);

            return (T) window;
        }
    }
}