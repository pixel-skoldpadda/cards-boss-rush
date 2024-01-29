using System.Collections.Generic;
using System.ComponentModel;
using Data;
using GameObjects.Character;
using Infrastructure.Services.State;
using Items.Card;
using TMPro;
using UnityEngine;

namespace Ui.Hud.Card
{
    public class CardsContainer : BaseHudContainer
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

        [SerializeField] private EndTurnButton endTurnButton;
        
        private readonly List<Card> cards = new();
        private IGameStateService _gameStateService;

        private CardsDeck _cardsDeck;
        
        public void Construct(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        protected override void ResetContainer()
        {
            base.ResetContainer();
            
            DestroyCards();
        }

        public void InitCardsDeck(CardsDeck cardsDeck)
        {
            _cardsDeck = cardsDeck;
            
            cardsDeck.OnCardsGenerated += OnCardsGenerated;
            cardsDeck.OnHangUp += OnHangUp;
            cardsDeck.OnCardGoOut += OnCardGoOut;
            cardsDeck.OnCardsDiscarding += OnCardsDiscarding;
        }

        private void OnCardGoOut()
        {
            cardsInOutCounter.text = $"{_cardsDeck.OutCardsCount}";
        }

        private void OnCardsGenerated()
        {
            AddCardsToDeck(_cardsDeck.CardsInHand);
            cardsInDeckCounter.text = $"{_cardsDeck.CardsCount}";
        }

        private void OnHangUp()
        {
            cardsInOutCounter.text = $"{_cardsDeck.OutCardsCount}";
            cardsInDeckCounter.text = $"{_cardsDeck.CardsCount}";
        }
        
        private void OnCardsDiscarding()
        {
            cardsInOutCounter.text = $"{_cardsDeck.OutCardsCount}";
            DestroyCards();
        }

        private void DestroyCards()
        {
            foreach (Card card in cards)
            {
                card.ChangeInteractionEnabled(false);
                Destroy(card.gameObject);
            }
            cards.Clear();
        }

        private void AddCardsToDeck(List<CardItem> cardItems)
        {
            foreach (CardItem cardItem in cardItems)
            {
                AddCard(cardItem);
            }
            AlignCards();
        }

        private void AddCard(CardItem cardItem)
        {
            GameObject cardGameObject = Instantiate(cardPrefab, transform);
                
            Card card = cardGameObject.GetComponent<Card>();
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

        private void OnCardClicked(Card clickedCard)
        {
            GameState gameState = _gameStateService.State;
            Character player = gameState.ActiveCharacter;
            CardsDeck cardsDeck = player.CardsDeck;

            if (!cardsDeck.CanUseCard())
            {
                //: todo show particle text
                return;
            }
            
            endTurnButton.ChangeInteractable(false);
            clickedCard.ChangeInteractionEnabled(false);
            cards.Remove(clickedCard);

            CardItem cardItem = clickedCard.CardItem;
            CardType cardType = cardItem.CardType;

            Vector3 position = Vector3.zero;
            if (CardType.Attack.Equals(cardType))
            {
                position = Camera.main.WorldToScreenPoint(gameState.GetOpponentCharacter().transform.position);
            }
            else if (CardType.Protection.Equals(cardType))
            {
                position = Camera.main.WorldToScreenPoint(player.transform.position);
            }
            
            cardsDeck.UpdateUsedCardsCounter();
            cardsDeck.RemoveCardsFromHand(cardItem);
            clickedCard.CardAnimator.MoveToAndDestroy(position, () =>
            {
                Destroy(clickedCard.gameObject);
                player.UseCard(cardItem);
                endTurnButton.ChangeInteractable(true);
            });
            
            AlignCards();
        }

        private void OnDestroy()
        {
            _cardsDeck.OnCardsGenerated -= OnCardsGenerated;
            _cardsDeck.OnHangUp -= OnHangUp;
            _cardsDeck.OnCardGoOut -= OnCardGoOut;
            _cardsDeck.OnCardsDiscarding -= OnCardsDiscarding;
        }
    }
}