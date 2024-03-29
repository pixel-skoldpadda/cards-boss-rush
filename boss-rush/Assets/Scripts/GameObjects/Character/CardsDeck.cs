﻿using System;
using System.Collections.Generic;
using Items.Card;
using ModestTree;

namespace GameObjects.Character
{
    [Serializable]
    public class CardsDeck
    {
        private readonly List<CardItem> _allCards = new();
        private readonly List<CardItem> _outCards = new();
        private readonly List<CardItem> _cardsInHand = new();
        private readonly List<CardItem> _cardsStack = new();
        private readonly List<CardItem> _defaultDeck = new();
        
        private readonly int _useCardsLimit;
        private readonly int _cardInHandCount;
        private readonly Statuses _statuses;

        private int _cardsUsed;
        
        // TODO Навести порядок избавиться от кучи слушателей
        public Action OnCardsGenerated { get; set; }
        public Action OnHangUp { get; set; }
        public Action OnCardGoOut { get; set; }
        public Action OnCardsDiscarding { get; set; }
        public Action<int, int> OnUsedCardsCountChanged { get; set; }
        
        public CardsDeck(List<CardItem> allCards, int cardsInHandCount, int useCardsLimit, Statuses statuses)
        {
            _defaultDeck.AddRange(allCards);
            _allCards.AddRange(allCards);
            _useCardsLimit = useCardsLimit;
            _cardInHandCount = cardsInHandCount;
            _statuses = statuses;
        }

        public void AddCard(CardItem cardItem)
        {
            _defaultDeck.Add(cardItem);
            _allCards.Add(cardItem);
        }
        
        public CardItem ExchangeCard(CardItem cardItem)
        {
            CardItem noSpecialCard = GetRandomNoSpecialCard(_cardsInHand, cardItem);
            if (noSpecialCard != null)
            {
                _cardsInHand.Remove(noSpecialCard);
                _cardsInHand.Add(cardItem);

                return noSpecialCard;
            }

            noSpecialCard = GetRandomNoSpecialCard(_allCards, cardItem);
            if (noSpecialCard != null)
            {
                _allCards.Remove(noSpecialCard);
                _allCards.Add(cardItem);

                return noSpecialCard;
            }
            
            noSpecialCard = GetRandomNoSpecialCard(_outCards, cardItem);
            if (noSpecialCard != null)
            {
                _outCards.Remove(noSpecialCard);
                _outCards.Add(cardItem);
            }
            
            return noSpecialCard;
        }
        
        public CardItem ChooseCardToStack(StatusType type, StatusSubtype subtype)
        {
            CardItem cardItem = null;
            foreach (CardItem item in _cardsInHand)
            {
                foreach (StatusItem statusItem in item.StatusItems)
                {
                    if (type.Equals(statusItem.Type) && subtype.Equals(statusItem.Subtype))
                    {
                        cardItem = item;
                        break;
                    }
                }
            }
            
            _cardsInHand.Remove(cardItem);
            _cardsStack.Add(cardItem);

            return cardItem;
        }
        
        public void DiscardingCards()
        {
            foreach (CardItem cardItem in _cardsInHand)
            {
                _outCards.Add(cardItem);
            }
            
            _cardsInHand.Clear();
            OnCardsDiscarding?.Invoke();
            
            _cardsUsed = 0;
            OnUsedCardsCountChanged?.Invoke(_cardsUsed, _useCardsLimit);
        }
        
        public void GeneratedCardsInHand()
        {
            TryHangUp();

            int cardsCount = _cardInHandCount;
            foreach (Status status in _statuses.ActiveStatuses)
            {
                if (StatusType.Confused.Equals(status.Item.Type))
                {
                    cardsCount -= status.Item.Value;
                }
            }

            Random random = new Random();
            for (int i = 0; i < cardsCount; i++)
            {
                int index = random.Next(0, _allCards.Count);
                _cardsInHand.Add(_allCards[index]);
                _allCards.RemoveAt(index);
            }
            
            OnCardsGenerated?.Invoke();
        }

        public List<CardItem> GetRandomThreeSpecialCards()
        {
            List<CardItem> allSpecialCards = new List<CardItem>();
            foreach (CardItem cardItem in _allCards)
            {
                if (cardItem.Special)
                {
                    allSpecialCards.Add(cardItem);
                }
            }

            int cardsCount = allSpecialCards.Count;
            if (cardsCount == 0)
            {
                Log.Error("Special card in deck cannot be null!");
                return null;
            }

            if (cardsCount <= 3)
            {
                return allSpecialCards;
            }

            List<CardItem> randomCards = new List<CardItem>(3);
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(0, allSpecialCards.Count);
                randomCards.Add(allSpecialCards[index]);
                allSpecialCards.RemoveAt(index);
            }

            return randomCards;
        }

        public void UpdateUsedCardsCounter()
        {
            _cardsUsed++;
        }

        public void RemoveCardsFromHand(CardItem cardItem)
        {
            OnUsedCardsCountChanged?.Invoke(_cardsUsed, _useCardsLimit);

            _cardsInHand.Remove(cardItem);    
            _outCards.Add(cardItem);
            
            OnCardGoOut?.Invoke();
        }

        public bool CanUseCard()
        {
            return _cardsUsed < _useCardsLimit;
        }

        public bool HasAnyCardInHandWithStatus(StatusType type, StatusSubtype subtype)
        {
            foreach (CardItem cardItem in _cardsInHand)
            {
                foreach (StatusItem statusItem in cardItem.StatusItems)
                {
                    if (statusItem.Type.Equals(type) && statusItem.Subtype.Equals(subtype))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Reset()
        {
            _allCards.Clear();
            _allCards.AddRange(_defaultDeck);
            
            _cardsUsed = 0;
            _cardsInHand.Clear();
            _outCards.Clear();
            _cardsStack.Clear();
            
            OnHangUp?.Invoke();
        }

        public List<CardItem> CardsInHand => _cardsInHand;
        public int CardsCount => _allCards.Count;
        public int OutCardsCount => _outCards.Count;
        public int UseCardsLimit => _useCardsLimit;
        public int CardsUsed => _cardsUsed;
        public List<CardItem> CardsStack => _cardsStack;

        private CardItem GetRandomNoSpecialCard(List<CardItem> cards, CardItem item)
        {
            List<CardItem> noSpecialCards = new List<CardItem>();
            foreach (CardItem cardItem in cards)
            {
                if (!cardItem.Special && !item.Equals(cardItem))
                {
                    noSpecialCards.Add(cardItem);
                }
            }

            if (noSpecialCards.IsEmpty())
            {
                return null;
            }
            
            Random random = new Random();
            int index = random.Next(0, noSpecialCards.Count);

            return noSpecialCards[index];
        }

        private void TryHangUp()
        {
            if (_allCards.Count < _cardInHandCount)
            {
                _allCards.AddRange(_outCards);
                _outCards.Clear();
                OnHangUp?.Invoke();
            }
        }
    }
}