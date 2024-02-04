using System;
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
        
        private readonly List<CardView> _cardViews = new();
        private IGameStateService _gameStateService;

        private CardsDeck _cardsDeck;

        private CardsContainerMode _mode = CardsContainerMode.Combat;

        public Action<CardView> OnCardSelected { get; set; }

        public void Construct(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public override void ResetContainer()
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

        public CardsContainerMode Mode
        {
            set => _mode = value;
        }

        public void ChangeCardsInteraction(bool interaction)
        {
            foreach (CardView cardView in _cardViews)
            {
                cardView.ChangeInteractionEnabled(interaction);
            }
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
            foreach (CardView card in _cardViews)
            {
                card.ChangeInteractionEnabled(false);
                Destroy(card.gameObject);
            }
            _cardViews.Clear();
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
                
            CardView cardView = cardGameObject.GetComponent<CardView>();
            cardView.Init(cardItem);
            cardView.OnCardClicked += OnCardClicked;
            _cardViews.Add(cardView);
        }

        private void AlignCards()
        {
            int size = _cardViews.Count;
            
            Vector2 position = new Vector2();
            position.x = -(xOffsetValue * size - xOffsetValue) / 2;
            Vector3 rotation = new Vector3();
            
            for (int i = 0; i < size; i++)
            {
                float cardsRatio = size > 1 ? i / (size - 1f) : .5f;
                position.y = yOffsetCurve.Evaluate(cardsRatio) * yOffsetValue;
                rotation.z = rotationCurve.Evaluate(cardsRatio) * rotationValue;
                
                _cardViews[i].CardAnimator.PlayPositioningAnimation(position, rotation, i);
                position.x += xOffsetValue;
            }
        }

        private void OnCardClicked(CardView clickedCardView)
        {
            if (CardsContainerMode.Combat.Equals(_mode))
            {
                TryUseCard(clickedCardView);
            }
            else if (CardsContainerMode.Exchange.Equals(_mode))
            {
                ChangeCardsInteraction(false);
                OnCardSelected?.Invoke(clickedCardView);
            }
        }

        private void TryUseCard(CardView clickedCardView)
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
            clickedCardView.ChangeInteractionEnabled(false);
            _cardViews.Remove(clickedCardView);

            CardItem cardItem = clickedCardView.CardItem;
            bool hasNegativeEffect = false;
            
            foreach (StatusItem statusItem in cardItem.StatusItems)
            {
                if (StatusSubtype.Negative.Equals(statusItem.Subtype))
                {
                    hasNegativeEffect = true;
                    break;
                }
            }

            Vector3 position;
            if (hasNegativeEffect)
            {
                position = Camera.main.WorldToScreenPoint(gameState.GetOpponentCharacter().transform.position);
            }
            else
            {
                position = Camera.main.WorldToScreenPoint(player.transform.position);
            }
            
            cardsDeck.UpdateUsedCardsCounter();
            cardsDeck.RemoveCardsFromHand(cardItem);
            clickedCardView.CardAnimator.MoveToAndDestroy(position, () =>
            {
                Destroy(clickedCardView.gameObject);
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