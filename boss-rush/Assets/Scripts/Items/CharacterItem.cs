using System.Collections.Generic;
using Items.Card;
using UnityEngine;

namespace Items
{
    public abstract class CharacterItem : GameObjectItem
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private List<CardItem> deck;

        public int MaxHealth => maxHealth;
        public List<CardItem> Deck => deck;
    }
}