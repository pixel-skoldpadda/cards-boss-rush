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
        
        private IGameStateService _gameStateService;
        private IWindowsManager _windowsManager;
        
        public void Construct(IGameStateService gameStateService, IWindowsManager windowsManager)
        {
            _gameStateService = gameStateService;
            _windowsManager = windowsManager;
        }

        public void OnButtonClicked()
        {
            Hud hud = _gameStateService.State.HUD;
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
    }
}