using Configs;
using Data;
using Infrastructure.Services.State;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Ui.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject continueButton;
        [SerializeField] private GameObject audioSettings;
        [SerializeField] private GameObject menu;

        private IGameStateService _gameStateService;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IGameStateService gameStateService)
        {
            _gameStateMachine = stateMachine;
            _gameStateService = gameStateService;
        }
        
        private void Start()
        {
            audioSettings.SetActive(false);
            continueButton.SetActive(_gameStateService.State != null);
        }

        
        public void OnContinueButtonClicked()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneConfig.GameScene);
        }

        public void OnNewGameButtonClicked()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            
            _gameStateService.State = new GameState();
            _gameStateMachine.Enter<LoadLevelState, string>(SceneConfig.GameScene);
        }
        
        public void OnOptionsButtonClicked()
        {
            menu.SetActive(false);
            audioSettings.SetActive(true);
        }

        public void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}