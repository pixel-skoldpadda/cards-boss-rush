using Data;
using DG.Tweening;
using Infrastructure.Services.State;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud
{
    public class EndTurnButton : BaseHudContainer
    {
        [SerializeField] private Button endTurnButton;
        
        private GameState _gameState;
        private Tweener _rotationTween;

        public void Construct(IGameStateService gameStateService)
        {
            _gameState = gameStateService.State;

            _gameState.OnTurnStarted += OnTurnStarted;
            _gameState.OnTurnFinished += OnTurnFinished;
        }

        public void ChangeInteractable(bool interactable)
        {
            endTurnButton.interactable = interactable;
        }

        public override void ResetContainer()
        {
            base.ResetContainer();

            endTurnButton.interactable = false;
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
            _gameState.ActiveCharacter.OnEndTurn?.Invoke();
        }

        private void OnDestroy()
        {
            _gameState.OnTurnStarted -= OnTurnStarted;
            _gameState.OnTurnFinished -= OnTurnFinished;
            _rotationTween?.Kill();
            _rotationTween = null;
        }
    }
}