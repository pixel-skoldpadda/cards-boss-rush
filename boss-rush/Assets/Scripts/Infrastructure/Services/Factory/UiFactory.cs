using Infrastructure.Services.Assets;
using Infrastructure.Services.Items;
using Infrastructure.Services.State;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States;
using Items;
using Ui.Hud;
using Ui.Hud.Boss;
using Ui.Hud.MiddleContainers;
using Ui.Windows;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class UiFactory : IUiFactory
    {
        private readonly IItemsService _items;
        private readonly DiContainer _diContainer;
        private readonly IGameStateMachine _stateMachine;
        private readonly IAssetsProvider _assets;
        private readonly IGameStateService _gameStateService;

        [Inject]
        public UiFactory(IItemsService items, IAssetsProvider assets, IGameStateService gameStateService, DiContainer diContainer)
        {
            _items = items;
            _assets = assets;
            _diContainer = diContainer;
            _gameStateService = gameStateService;
        }
        
        public T CreateWindow<T>(WindowType type, IWindowsManager windowsManager, object[] args) where T : Window
        {
            WindowItem item = _items.GetWindowItem(type);
            Window window = _diContainer.InstantiatePrefabForComponent<T>(item.Prefab);

            return (T) window;
        }
        
        public void CreateHud()
        {
            GameObject prefab = _assets.LoadResource(AssetsPath.HudPrefabPath, false);
            Hud hud = Object.Instantiate(prefab).GetComponent<Hud>();

            hud.CardsContainer.Construct(_gameStateService);
            hud.EndTurnButton.Construct(_gameStateService);
            
            _gameStateService.State.HUD = hud;
        }
    }
}