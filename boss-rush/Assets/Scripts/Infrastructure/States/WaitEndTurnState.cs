﻿using Data;
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
            Character character = gameState.ActiveCharacter;
            
            character.OnEndTurn += OnTurnEnded;
            gameState.OnTurnStarted?.Invoke();
        }

        private void OnTurnEnded()
        {
            _stateMachine.Enter<StepTransitionState>();
        }
        
        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
            
            Character character = _gameStateService.State.ActiveCharacter;
            character.CardsDeck.DiscardingCards();
            character.OnEndTurn -= OnTurnEnded;
            
            _gameStateService.State.OnTurnFinished?.Invoke();
        }
    }
}