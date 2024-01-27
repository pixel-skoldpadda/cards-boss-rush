using Data;
using GameObjects.Character;
using Infrastructure.Services.State;
using Infrastructure.States.Interfaces;
using Ui.Hud;
using Ui.Hud.MiddleContainers;
using UnityEngine;

namespace Infrastructure.States
{
    public class GenerateCardsInHandState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameStateService _gameStateService;

        public GenerateCardsInHandState(IGameStateMachine stateMachine, IGameStateService gameStateService)
        {
            _stateMachine = stateMachine;
            _gameStateService = gameStateService;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            GameState gameState = _gameStateService.State;

            //: TODO Костыль для генерации карт в руки босса (разрулить иначе)!
            Character character = gameState.ActiveCharacter;
            if (character.IsPlayer())
            {
                character.CardsDeck.GeneratedCardsInHand();
                gameState.GetOpponentCharacter().CardsDeck.GeneratedCardsInHand();
            }

            StepContainer stepContainer = gameState.HUD.StepContainer;
            stepContainer.Hide(() =>
            {
                _stateMachine.Enter<WaitEndTurnState>();
            });
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}