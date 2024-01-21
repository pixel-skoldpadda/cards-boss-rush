using System.Collections.Generic;
using Items.Card;

namespace GameObjects.Character
{
    public class CardsDeck
    {
        private List<CardItem> _allCards;
        private List<CardItem> _outCards;
        private List<CardItem> _cardsInHand;
        
        public CardsDeck(List<CardItem> allCards)
        {
            _allCards = allCards;
        }
    }
}