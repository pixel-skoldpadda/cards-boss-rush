using Infrastructure.Services.State;
using Infrastructure.Services.WindowsManager;
using Ui.Hud;
using Ui.Hud.Card;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class HudInstaller : MonoInstaller
    {
        [SerializeField] private GameObject hudPrefab;

        private IGameStateService _gameStateService;
        private IWindowsManager _windowsManager;
        
        [Inject]
        private void Construct(IGameStateService gameStateService, IWindowsManager windowsManager)
        {
            _gameStateService = gameStateService;
            _windowsManager = windowsManager;
        }
        
        public override void InstallBindings()
        {
            Hud hud = Container.InstantiatePrefabForComponent<Hud>(hudPrefab);
            _gameStateService.State.HUD = hud;
            
            CardsContainer cardsContainer = hud.CardsContainer;
            cardsContainer.Construct(_gameStateService);
            hud.EndTurnButton.Construct(_gameStateService);
            hud.ExchangeButton.Construct(_gameStateService, _windowsManager);
            hud.SettingsContainer.Construct(_windowsManager, _gameStateService);
        }
    }
}