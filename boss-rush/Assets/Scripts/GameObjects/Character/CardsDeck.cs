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

        public Action OnCardsGenerated { get; set; }
        public Action OnHangUp { get; set; }

        public CardsDeck(List<CardItem> allCards, int attackCardsCount, int protectionCardsCount)
        {
            _attackCardsCount = attackCardsCount;
            _protectionCardsCount = protectionCardsCount;
            
            DistributeCardsByType(allCards);
        }

        public void GeneratedCardsInHand()
        {
            TryHangUp();
            
            PutCardsInHand(_attackCards, _attackCardsCount);
            PutCardsInHand(_protectionCards, _protectionCardsCount);
            
            OnCardsGenerated?.Invoke();
        }

        public List<CardItem> CardsInHand => _cardsInHand;

        public int GetCardsCount()
        {
            return _protectionCards.Count + _attackCards.Count;
        }

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