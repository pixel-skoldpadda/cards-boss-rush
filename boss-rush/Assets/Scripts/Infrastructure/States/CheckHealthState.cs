using Configs;
using Data;
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
    // TODO: Переименовать стейт ОТРЕФАКТОРИТЬ!
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

            GameState gameState = _gameStateService.State;

            Character character = gameState.GetOpponentCharacter();
            character.Animator.PlayDeathAnimation();
            character.CardsDeck.Reset();
            
            if (character.IsPlayer())
            {
                ShowDeathContainer();
            }
            else
            {
                gameState.ActiveCharacter.ResetState();
                gameState.ActiveCharacter = null;

                gameState.HUD.BossCardsContainer.ClearAllCards();
                gameState.Characters.Remove(character);
                
                _windowsManager.OpenWindow(
                    WindowType.ChooseCardWindow, 
                    false, 
                    () => _stateMachine.Enter<SpawnBossEnemyState>(), 
                    character.CardsDeck.GetRandomThreeSpecialCards());
            }
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