using System.Collections.Generic;
using Data;
using GameObjects.Character;
using Infrastructure.Services.State;
using Items.Card;
using ModestTree;
using Ui.Hud.Card;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Ui.Windows.Exchange
{
    public class ExchangeWindow : Window
    {
        [SerializeField] private GameObject grid;
        [SerializeField] private GameObject chooseCardHint;
        
        private GameState _gameState;

        [Inject]
        private void Construct(IGameStateService gameStateService)
        {
            _gameState = gameStateService.State;
        }

        public void OnExchangeCardButtonClick()
        {
            Hud.Hud hud = _gameState.HUD;
            
            grid.SetActive(false);
            chooseCardHint.SetActive(true);
            
            CardsContainer cardsContainer = hud.CardsContainer;
            cardsContainer.Mode = CardsContainerMode.Exchange;
            cardsContainer.OnCardSelected += OnCardSelected;
            cardsContainer.Show();
        }

        private void OnCardSelected(CardView cardView)
        {
            CardItem cardItem = cardView.CardItem;

            Character player = _gameState.ActiveCharacter;
            CardsDeck playerCardsDeck = player.CardsDeck;
            
            playerCardsDeck.CardsInHand.Remove(cardItem);

            Character enemy = _gameState.GetOpponentCharacter();
            CardItem enemyCard = enemy.CardsDeck.ExchangeCard(cardItem);
            
            cardView.CardAnimator.PlayShockwaveAnimation();
            cardView.Init(enemyCard);
            playerCardsDeck.CardsInHand.Add(enemyCard);

            CardsContainer cardsContainer = _gameState.HUD.CardsContainer;
            cardsContainer.OnCardSelected -= OnCardSelected;
            cardsContainer.Mode = CardsContainerMode.Combat;
            cardsContainer.ChangeCardsInteraction(true);
            
            Close();
        }

        public void OnExchangeStatusButtonClick()
        {
            Character player = _gameState.ActiveCharacter;
            Statuses playerStatuses = player.Statuses;
            Status playerStatus = GetRandomStatus(player);

            Character enemy = _gameState.GetOpponentCharacter();
            Statuses enemyStatuses = enemy.Statuses;
            Status enemyStatus = GetRandomStatus(enemy);
            
            if (playerStatus != null)
            {
                playerStatuses.RemoveStatus(playerStatus);
                enemyStatuses.AddStatus(playerStatus);
            }

            if (enemyStatus != null)
            {
                enemyStatuses.RemoveStatus(enemyStatus);
                playerStatuses.AddStatus(enemyStatus);
            }
            
            Close();
        }

        private Status GetRandomStatus(Character character)
        {
            Statuses statuses = character.Statuses;
            List<Status> statusList = statuses.ActiveStatuses;
            if (statusList.IsEmpty())
            {
                return null;
            }
            
            Random random = new Random();
            int size = statusList.Count;

            return statusList[random.Next(0, size)];
        }
    }
}