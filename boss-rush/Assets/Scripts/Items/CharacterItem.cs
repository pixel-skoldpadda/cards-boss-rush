using System.Collections.Generic;
using Items.Card;
using UnityEngine;

namespace Items
{
    public abstract class CharacterItem : GameObjectItem
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private List<CardItem> deck;
        [SerializeField] private string stepDescription;

        [Space(10)]
        [Header("Deck settings")]
        [SerializeField] private int cardsOnHand = 5;
        [SerializeField] private int useCardsLimit = 3;

        [Space(10)] 
        [Header("Exchange settings")]
        [SerializeField]
        private int useExchangeLimit;
        
        public int MaxHealth => maxHealth;
        public List<CardItem> Deck => deck;
        public int CardsOnHand => cardsOnHand;
        public int UseCardsLimit => useCardsLimit;
        public string StepDescription => stepDescription;
        public int UseExchangeLimit => useExchangeLimit;
    }
}