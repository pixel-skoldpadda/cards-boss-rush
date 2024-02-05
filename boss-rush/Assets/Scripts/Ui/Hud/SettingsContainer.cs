using Infrastructure.Services.State;
using Infrastructure.Services.WindowsManager;
using Ui.Windows;

namespace Ui.Hud
{
    public class SettingsContainer : BaseHudContainer
    {
        private IWindowsManager _windowsManager;
        private IGameStateService _gameStateService;

        public void Construct(IWindowsManager windowsManager, IGameStateService gameStateService)
        {
            _windowsManager = windowsManager;
            _gameStateService = gameStateService;
        }

        public void OnButtonClicked()
        {
            Hud hud = _gameStateService.State.HUD;
            hud.Hide();
            
            _windowsManager.OpenWindow(WindowType.Pause, false, () =>
            {
                hud.Show();
            });
        }
    }
}