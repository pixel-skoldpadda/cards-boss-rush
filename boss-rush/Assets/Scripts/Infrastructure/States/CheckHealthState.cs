using Configs;
using DG.Tweening;
using GameObjects.Character;
using Infrastructure.Services.State;
using Infrastructure.States.Interfaces;
using Ui.Hud.MiddleContainers;
using UnityEngine;

namespace Infrastructure.States
{
    public class CheckHealthState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameStateService _gameStateService;

        public CheckHealthState(IGameStateMachine stateMachine, IGameStateService gameStateService)
        {
            _stateMachine = stateMachine;
            _gameStateService = gameStateService;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            Character character = _gameStateService.State.GetOpponentCharacter();
            if (character.Health <= 0)
            {
                if (character.IsPlayer())
                {
                    ShowDeathContainer();
                }
                else
                {
                    
                }
            }
            else
            {
                _stateMachine.Enter<StepTransitionState>();
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