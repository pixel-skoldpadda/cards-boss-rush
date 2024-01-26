using Data;
using GameObjects.Character;
using Infrastructure.Services.State;
using Infrastructure.States.Interfaces;
using UnityEngine;

namespace Infrastructure.States
{
    public class WaitEndTurnState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IGameStateService _gameStateService;

        public WaitEndTurnState(IGameStateMachine stateMachine, IGameStateService gameStateService)
        {
            _stateMachine = stateMachine;
            _gameStateService = gameStateService;
        }
        
        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            GameState gameState = _gameStateService.State;
            gameState.OnTurnStarted?.Invoke();

            Character character = gameState.ActiveCharacter;
            character.OnEndTurn += () =>
            {
                character.CardsDeck.DiscardingCards();
                _stateMachine.Enter<StepTransitionState>();
            };
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
            
            _gameStateService.State.OnTurnFinished?.Invoke();
        }
    }
}