using Infrastructure.Services.Assets;
using Infrastructure.Services.Items;
using Infrastructure.Services.State;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States;
using Items;
using Ui.Hud;
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
            Hud hud = _diContainer.InstantiatePrefabForComponent<Hud>(prefab);
            _gameStateService.State.HUD = hud;
            
            CardsContainer cardsContainer = hud.CardsContainer;
            cardsContainer.Construct(_gameStateService);
            hud.EndTurnButton.Construct(_gameStateService);

            _diContainer.Bind<BossHealthBar>().FromInstance(hud.BossHealthBar);
            _diContainer.Bind<CardsContainer>().FromInstance(cardsContainer);
            _diContainer.Bind<StepContainer>().FromInstance(hud.StepContainer);
            _diContainer.Bind<CardsLimitContainer>().FromInstance(hud.CardsLimitContainer);
        }
    }
}