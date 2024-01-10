using Configs;
using Infrastructure.Services.Audio;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.State;
using Infrastructure.States.Interfaces;
using UnityEngine;
using AudioSettings = Data.AudioSettings;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameStateService _gameStateService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAudioService _audio;

        public LoadProgressState(GameStateMachine stateMachine, IGameStateService gameStateService, ISaveLoadService saveLoadService, IAudioService audio)
        {
            _stateMachine = stateMachine;
            _gameStateService = gameStateService;
            _saveLoadService = saveLoadService;
            _audio = audio;
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            _audio.Settings = _saveLoadService.LoadAudioSettings() ?? new AudioSettings();
            _gameStateService.State = _saveLoadService.LoadGameState();
            
            _stateMachine.Enter<LoadSceneState, string>(SceneConfig.MenuScene);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}