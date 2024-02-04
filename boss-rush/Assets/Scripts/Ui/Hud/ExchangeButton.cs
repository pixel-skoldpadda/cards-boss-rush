using Data;
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
        }

        public void OnButtonClicked()
        {
            exchangeButton.interactable = false;
            _gameState.GetPlayer().Exchange--;
            
            Hud hud =_gameState.HUD;
            hud.Hide();
            
            _windowsManager.OpenWindow(WindowType.ExchangeWindow, false, () =>
            {
                hud.Show();
            });
        }

        public void UpdateExchangeCounter(int current, int max)
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