using System;
using System.Collections.Generic;
using Data;
using GameObjects.Character;
using Infrastructure.Services.State;
using ModestTree;
using Ui.Hud.Card;
using Zenject;

namespace Ui.Windows.Exchange
{
    public class ExchangeWindow : Window
    {
        private GameState _gameState;

        [Inject]
        private void Construct(IGameStateService gameStateService)
        {
            _gameState = gameStateService.State;
        }

        public void OnExchangeCardButtonClick()
        {
            Hud.Hud hud = _gameState.HUD;
            
            CardsContainer cardsContainer = hud.CardsContainer;
            cardsContainer.Show();
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