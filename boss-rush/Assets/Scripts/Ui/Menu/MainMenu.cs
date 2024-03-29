﻿using Configs;
using Data;
using Infrastructure.Services.State;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Ui.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menu;

        private IGameStateService _gameStateService;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IGameStateService gameStateService)
        {
            _gameStateMachine = stateMachine;
            _gameStateService = gameStateService;
        }
        
        public void OnNewGameButtonClicked()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            
            _gameStateService.State = new GameState();
            _gameStateMachine.Enter<LoadLevelState, string>(SceneConfig.GameScene);
        }

        public void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}