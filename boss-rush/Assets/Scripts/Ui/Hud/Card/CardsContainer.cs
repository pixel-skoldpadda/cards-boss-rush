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
        
        private readonly List<CardView> cardViews = new();
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
            foreach (CardView card in cardViews)
            {
                card.ChangeInteractionEnabled(false);
                Destroy(card.gameObject);
            }
            cardViews.Clear();
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
            cardViews.Add(cardView);
        }

        private void AlignCards()
        {
            int size = cardViews.Count;
            
            Vector2 position = new Vector2();
            position.x = -(xOffsetValue * size - xOffsetValue) / 2;
            Vector3 rotation = new Vector3();
            
            for (int i = 0; i < size; i++)
            {
                float cardsRatio = size > 1 ? i / (size - 1f) : .5f;
                position.y = yOffsetCurve.Evaluate(cardsRatio) * yOffsetValue;
                rotation.z = rotationCurve.Evaluate(cardsRatio) * rotationValue;
                
                cardViews[i].CardAnimator.PlayPositioningAnimation(position, rotation, i);
                position.x += xOffsetValue;
            }
        }

        private void OnCardClicked(CardView clickedCardView)
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
            cardViews.Remove(clickedCardView);

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