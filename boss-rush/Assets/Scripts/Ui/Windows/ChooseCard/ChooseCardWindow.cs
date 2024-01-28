using System.Collections.Generic;
using Infrastructure.Services.State;
using Items.Card;
using UnityEngine;
using Zenject;

namespace Ui.Windows.ChooseCard
{
    public class ChooseCardWindow : Window
    {
        [SerializeField] private GameObject selectiveCardPrefab;
        [SerializeField] private Transform grid;

        private readonly List<SelectiveCard> _cards = new();

        private IGameStateService _gameStateService;
        
        [Inject]
        private void Construct(List<CardItem> cardItems, IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
            
            foreach (CardItem cardItem in cardItems)
            {
                SelectiveCard card = Instantiate(selectiveCardPrefab, grid).GetComponent<SelectiveCard>();
                card.Init(cardItem, this);
                
                _cards.Add(card);
            }
        }

        public void OnCardClicked(CardItem cardItem)
        {
            foreach (SelectiveCard card in _cards)
            {
                card.DisableInteraction();
            }
            
            _gameStateService.State.PlayerCards.Add(cardItem);
            Close();
        }
    }
}