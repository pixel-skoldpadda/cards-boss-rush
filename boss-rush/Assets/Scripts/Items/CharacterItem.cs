using System.Collections.Generic;
using System.ComponentModel;
using Items.Card;
using UnityEngine;

namespace Items
{
    public abstract class CharacterItem : GameObjectItem
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private List<CardItem> deck;
        [SerializeField] private int useCardsLimit = 3;
        [SerializeField] private string stepDescription;

        [Space(10)]
        [Description("Deck settings")]
        [SerializeField] private int cardsOnHand;
        [SerializeField] private int protectionCards;
        [SerializeField] private int attackCards;

        public int MaxHealth => maxHealth;
        public List<CardItem> Deck => deck;
        public int CardsOnHand => cardsOnHand;
        public int ProtectionCards => protectionCards;
        public int AttackCards => attackCards;
        public int UseCardsLimit => useCardsLimit;
        public string StepDescription => stepDescription;
    }
}