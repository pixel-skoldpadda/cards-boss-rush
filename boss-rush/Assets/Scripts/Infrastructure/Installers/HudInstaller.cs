using Infrastructure.Services.State;
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
        
        [Inject]
        private void Construct(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
        
        public override void InstallBindings()
        {
            Hud hud = Container.InstantiatePrefabForComponent<Hud>(hudPrefab);
            _gameStateService.State.HUD = hud;
            
            CardsContainer cardsContainer = hud.CardsContainer;
            cardsContainer.Construct(_gameStateService);
            hud.EndTurnButton.Construct(_gameStateService);
        }
    }
}