using Configs;
using DG.Tweening;
using GameObjects.Character;
using Infrastructure.Services.State;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States.Interfaces;
using Ui.Hud.MiddleContainers;
using Ui.Windows;
using UnityEngine;

namespace Infrastructure.States
{
    // TODO: Переименовать стейт
    public class CheckHealthState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameStateService _gameStateService;
        private readonly IWindowsManager _windowsManager;

        public CheckHealthState(IGameStateMachine stateMachine, IGameStateService gameStateService, IWindowsManager windowsManager)
        {
            _stateMachine = stateMachine;
            _gameStateService = gameStateService;
            _windowsManager = windowsManager;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            Character character = _gameStateService.State.GetOpponentCharacter();
            character.Animator.PlayDeathAnimation(() =>
            {
                if (character.IsPlayer())
                {
                    ShowDeathContainer();
                }
                else
                {
                    _windowsManager.OpenWindow(
                        WindowType.ChooseCardWindow, false, null, character.CardsDeck.GetRandomThreeSpecialCards());
                }
            });
        }

        private void ShowDeathContainer()
        {
            TweenCallback loadMenu = () => _stateMachine.Enter<LoadSceneState, string>(SceneConfig.MenuScene);

            BaseMiddleContainer deathContainer = _gameStateService.State.HUD.DeathContainer;
            deathContainer.Show(() => deathContainer.Hide(loadMenu));
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}