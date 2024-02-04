using Data;
using GameObjects.Character.Player;
using Infrastructure.Services.State;
using Infrastructure.Services.WindowsManager;
using TMPro;
using Ui.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud
{
    public class ExchangeButton : BaseHudContainer
    {
        [SerializeField] private Button exchangeButton;
        [SerializeField] private TextMeshProUGUI exchangeCount;

        private GameState _gameState;
        private IWindowsManager _windowsManager;

        public void Construct(IGameStateService gameStateService, IWindowsManager windowsManager)
        {
            _gameState = gameStateService.State;
            _windowsManager = windowsManager;
            
            _gameState.OnTurnStarted += OnTurnStarted;
            _gameState.OnTurnFinished += OnTurnFinished;
            
            UpdateExchangeCounter(3,3);
        }

        public void OnButtonClicked()
        {
            Player player = _gameState.GetPlayer();

            player.Exchange--;

            int exchange = player.Exchange;
            exchangeButton.interactable = exchange > 0;
            UpdateExchangeCounter(exchange, player.Item.UseExchangeLimit);
            
            Hud hud =_gameState.HUD;
            hud.Hide();
            
            _windowsManager.OpenWindow(WindowType.ExchangeWindow, false, () =>
            {
                hud.Show();
            });
        }

        private void UpdateExchangeCounter(int current, int max)
        {
            exchangeCount.text = $"{current}/{max}";
        }

        private void OnTurnStarted()
        {
            exchangeButton.interactable = _gameState.ActiveCharacter.IsPlayer() && _gameState.GetPlayer().Exchange > 0;
        }

        private void OnTurnFinished()
        {
            exchangeButton.interactable = false;
        }

        private void OnDestroy()
        {
            _gameState.OnTurnStarted -= OnTurnStarted;
            _gameState.OnTurnFinished -= OnTurnFinished;
        }
    }
}