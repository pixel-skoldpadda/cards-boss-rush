using Configs;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States;
using Zenject;

namespace Ui.Windows.Congratulations
{
    public class CongratulationsWindow : Window
    {
        private IGameStateMachine _stateMachine;
        
        [Inject]
        private void Construct(IWindowsManager windowsManager, IGameStateMachine stateMachine)
        {
            base.Construct(windowsManager);

            _stateMachine = stateMachine;
        }

        public void OnExitToMenuButtonPressed()
        {
            Close();
            _stateMachine.Enter<LoadSceneState, string>(SceneConfig.MenuScene);
        }
    }
}