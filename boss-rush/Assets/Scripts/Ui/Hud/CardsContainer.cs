using System.Collections.Generic;
using Items.Card;
using TMPro;
using UnityEngine;

namespace Ui.Hud
{
    public class CardsContainer : MonoBehaviour
    {
        [SerializeField] private float offsetY;
        [SerializeField] private float spacingX;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private TextMeshProUGUI cardsInDeckCounter;
        [SerializeField] private TextMeshProUGUI cardsInOutCounter;

        private readonly List<Card.Card> cards = new();

        private Card.Card _selectedCard;

        public void AddCards(List<CardItem> cardItems)
        {
            for (var i = 0; i < cardItems.Count; i++)
            {
                GameObject cardGameObject = Instantiate(cardPrefab, transform);
                
                Card.Card card = cardGameObject.GetComponent<Card.Card>();
                card.Init(cardItems[i]);
                card.OnCardClicked += OnCardClicked;
                
                cards.Add(card);
            }

            AlignCards();
        }

        private void AlignCards()
        {
            int size = cards.Count;
            
            Vector2 position = new Vector3();
            position.y = offsetY;
            position.x = -(spacingX * size - spacingX) / 2;
            
            for (var i = 0; i < size; i++)
            {
                cards[i].CardAnimator.MoveToPosition(position, i);
                position.x += spacingX;
            }
        }

        private void OnCardClicked(Card.Card clickedCard)
        {
            cards.Remove(clickedCard);
            Destroy(clickedCard.gameObject);
            
            AlignCards();
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