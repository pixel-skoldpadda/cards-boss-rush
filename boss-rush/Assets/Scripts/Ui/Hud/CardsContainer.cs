using System.Collections.Generic;
using Items.Card;
using TMPro;
using UnityEngine;

namespace Ui.Hud
{
    public class CardsContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cardsInDeckCounter;
        [SerializeField] private TextMeshProUGUI cardsInOutCounter;

        [SerializeField] private List<Card.Card> cards;

        private Card.Card _selectedCard;
        
        public void UpdateCards(List<CardItem> cardItems)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                Card.Card card = cards[i];
                card.Init(cardItems[i], i);

                card.OnCardPickUp += OnCardPickUp;
                card.OnCardPickDown += OnCardPickDown;
            }
        }

        private void OnCardPickDown(int cardIndex)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                if (cardIndex != i)
                {
                    cards[i].CardAnimator.ResetPosition();
                }
            }
        }

        private void OnCardPickUp(int cardIndex)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                if (i == cardIndex)
                {
                    continue;
                }
                
                if (i < cardIndex)
                {
                    cards[i].CardAnimator.MoveToLeft();
                }
                else
                {
                    cards[i].CardAnimator.MoveToRight();
                }
            }
        }

        public void UpdateCardsInDeckCounter(int count)
        {
            cardsInDeckCounter.text = $"{count}";
        }
        
        public void UpdateCardsInOutCounter(int count)
        {
            cardsInOutCounter.text = $"{count}";
        }
    }
}