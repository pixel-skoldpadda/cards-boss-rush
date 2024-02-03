using Data;
using Infrastructure.Services.State;
using Ui.Hud.Card;
using Zenject;

namespace Ui.Windows.Exchange
{
    public class ExchangeWindow : Window
    {
        private IGameStateService _gameStateService;

        [Inject]
        private void Construct(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public void OnExchangeCardButtonClick()
        {
            GameState gameState = _gameStateService.State;
            Hud.Hud hud = gameState.HUD;
            
            CardsContainer cardsContainer = hud.CardsContainer;
            cardsContainer.Show();
        }
        
        public void OnExchangeStatusButtonClick()
        {
            
        }
    }
}