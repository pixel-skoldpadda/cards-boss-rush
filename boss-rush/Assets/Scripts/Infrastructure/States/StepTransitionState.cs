using System.Collections.Generic;
using Data;
using GameObjects.Character;
using Infrastructure.Services.State;
using Infrastructure.States.Interfaces;
using UnityEngine;

namespace Infrastructure.States
{
    public class StepTransitionState : IState
    {
        private readonly IGameStateService _gameStateService;
        private readonly IGameStateMachine _stateMachine;
        
        public StepTransitionState(IGameStateMachine stateMachine, IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
            
            GameState gameState = _gameStateService.State;

            Character currentCharacter = gameState.ActiveCharacter;
            if (currentCharacter == null)
            {
                gameState.CharacterIndex = 0;
                gameState.ActiveCharacter = gameState.Characters[gameState.CharacterIndex];
            }
            else
            {
                List<Character> characters = gameState.Characters;
                int currentIndex = gameState.CharacterIndex;

                currentIndex = currentIndex + 1 > characters.Count - 1 ? 0 : currentIndex + 1;
                gameState.CharacterIndex = currentIndex;

                gameState.ActiveCharacter = characters[currentIndex];
            }

            Character activeCharacter = gameState.ActiveCharacter;
            activeCharacter.Shield = 0;
            
            gameState.HUD.StepContainer.ShowAndHide(activeCharacter.Item.StepDescription, () =>
            {
                _stateMachine.Enter<GenerateCardsInHandState>();
            });
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}