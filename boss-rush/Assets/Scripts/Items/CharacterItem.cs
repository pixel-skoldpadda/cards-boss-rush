using Items.Card;
using UnityEngine;

namespace Items
{
    public abstract class CharacterItem : GameObjectItem
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private CardItem[] deck;

        public int MaxHealth => maxHealth;
        public CardItem[] Deck => deck;
    }
}