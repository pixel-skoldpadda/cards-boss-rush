using Configs;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States;
using Zenject;

namespace Ui.Windows.Pause
{
    public class PauseWindow : Window
    {
        private IGameStateMachine _stateMachine;

        [Inject]
        public void Construct(IWindowsManager windowsManager, IGameStateMachine stateMachine)
        {
            base.Construct(windowsManager);

            _stateMachine = stateMachine;
        }
        
        public void OnContinueButtonClicked()
        {
            Close();
        }

        public void OnExitToMenuButtonClicked()
        {
            OnWindowClosed += LoadMenuScene;
            Close();
        }

        private void LoadMenuScene()
        {
            _stateMachine.Enter<LoadSceneState, string>(SceneConfig.MenuScene);
        }
    }
}