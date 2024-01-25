using System.Collections.Generic;
using System.ComponentModel;
using Items.Card;
using TMPro;
using UnityEngine;

namespace Ui.Hud
{
    public class CardsContainer : MonoBehaviour
    {
        [Description("Cards rotation settings")]
        [SerializeField] private AnimationCurve rotationCurve;
        [SerializeField] private float rotationValue;
        
        [Space(10)] 
        [Description("Cards position settings")] 
        [SerializeField] private AnimationCurve yOffsetCurve;
        [SerializeField] private float yOffsetValue;
        [SerializeField] private float xOffsetValue;
        
        [Space(10)]
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private TextMeshProUGUI cardsInDeckCounter;
        [SerializeField] private TextMeshProUGUI cardsInOutCounter;

        private readonly List<Card.Card> cards = new();

        public void AddCardsToDeck(List<CardItem> cardItems)
        {
            foreach (CardItem cardItem in cardItems)
            {
                AddCard(cardItem);
            }
            AlignCards();
        }

        public void AddCard(CardItem cardItem)
        {
            GameObject cardGameObject = Instantiate(cardPrefab, transform);
                
            Card.Card card = cardGameObject.GetComponent<Card.Card>();
            card.Init(cardItem);
            card.OnCardClicked += OnCardClicked;
            cards.Add(card);
        }

        private void AlignCards()
        {
            int size = cards.Count;
            
            Vector2 position = new Vector2();
            position.x = -(xOffsetValue * size - xOffsetValue) / 2;
            Vector3 rotation = new Vector3();
            
            for (int i = 0; i < size; i++)
            {
                float cardsRatio = size > 1 ? i / (size - 1f) : .5f;
                position.y = yOffsetCurve.Evaluate(cardsRatio) * yOffsetValue;
                rotation.z = rotationCurve.Evaluate(cardsRatio) * rotationValue;
                
                cards[i].CardAnimator.PlayPositioningAnimation(position, rotation, i);
                position.x += xOffsetValue;
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