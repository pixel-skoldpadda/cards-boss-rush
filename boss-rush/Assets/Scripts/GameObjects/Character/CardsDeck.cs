using System;
using System.Collections.Generic;
using Items.Card;

namespace GameObjects.Character
{
    public class CardsDeck
    {
        private readonly List<CardItem> _outCards = new();
        private readonly List<CardItem> _cardsInHand = new();

        private readonly List<CardItem> _protectionCards = new();
        private readonly List<CardItem> _attackCards =  new();
        
        private readonly int _attackCardsCount;
        private readonly int _protectionCardsCount;
        private readonly int _useCardsLimit;

        private int _cardsUsed;
        
        public Action OnCardsGenerated { get; set; }
        public Action OnHangUp { get; set; }
        public Action OnCardGoOut { get; set; }
        public Action OnCardsDiscarding { get; set; }
        public Action<int, int> OnUsedCardsCountChanged { get; set; }

        public CardsDeck(List<CardItem> allCards, int attackCardsCount, int protectionCardsCount, int useCardsLimit)
        {
            _attackCardsCount = attackCardsCount;
            _protectionCardsCount = protectionCardsCount;
            _useCardsLimit = useCardsLimit;
            
            DistributeCardsByType(allCards);
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
            
            PutCardsInHand(_attackCards, _attackCardsCount);
            PutCardsInHand(_protectionCards, _protectionCardsCount);
            
            OnCardsGenerated?.Invoke();
        }

        public void RemoveCardsFromHand(CardItem cardItem)
        {
            _cardsUsed++;
            OnUsedCardsCountChanged?.Invoke(_cardsUsed, _useCardsLimit);
            
            _cardsInHand.Remove(cardItem);
            _outCards.Add(cardItem);
            
            OnCardGoOut?.Invoke();
        }

        public bool CanUseCard()
        {
            return _cardsUsed < _useCardsLimit;
        }
        
        public List<CardItem> CardsInHand => _cardsInHand;

        private void PutCardsInHand(List<CardItem> cards, int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int index = random.Next(0, cards.Count);
                _cardsInHand.Add(cards[index]);
                cards.RemoveAt(index);
            }
        }

        public int CardsCount => _protectionCards.Count + _attackCards.Count;
        public int OutCardsCount => _outCards.Count;
        
        public int UseCardsLimit => _useCardsLimit;
        public int CardsUsed => _cardsUsed;

        private void TryHangUp()
        {
            if (_protectionCards.Count < _protectionCardsCount || _attackCards.Count < _attackCardsCount)
            {
                DistributeCardsByType(_outCards);
                _outCards.Clear();
                OnHangUp?.Invoke();
            }
        }

        private void DistributeCardsByType(List<CardItem> allCards)
        {
            foreach (CardItem cardItem in allCards)
            {
                if (CardType.Attack.Equals(cardItem.CardType))
                {
                    _attackCards.Add(cardItem);
                }
                else
                {
                    _protectionCards.Add(cardItem);
                }
            }
        }
    }
}