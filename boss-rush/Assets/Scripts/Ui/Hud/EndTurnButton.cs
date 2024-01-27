﻿using Data;
using GameObjects.Character;
using Infrastructure.Services.State;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud
{
    public class EndTurnButton : MonoBehaviour
    {
        [SerializeField] private Button endTurnButton;
        
        private GameState _gameState;
        
        public void Construct(IGameStateService gameStateService)
        {
            _gameState = gameStateService.State;

            _gameState.OnTurnStarted += OnTurnStarted;
            _gameState.OnTurnFinished += OnTurnFinished;
        }

        private void OnTurnStarted()
        {
            endTurnButton.interactable = _gameState.ActiveCharacter.IsPlayer();
        }

        private void OnTurnFinished()
        {
            endTurnButton.interactable = false;
        }

        public void OnButtonClicked()
        {
            Character character = _gameState.ActiveCharacter;
            character.OnEndTurn?.Invoke();
        }

        private void OnDestroy()
        {
            _gameState.OnTurnStarted -= OnTurnStarted;
            _gameState.OnTurnFinished -= OnTurnFinished;
        }
    }
}