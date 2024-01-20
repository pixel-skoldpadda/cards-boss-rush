using UnityEngine;

namespace Items.Card
{
    [CreateAssetMenu(fileName = "CARD_ITEM", menuName = "Items/Card", order = 0)]
    public class CardItem : Item
    {
        [SerializeField] private CardType cardType;
        [SerializeField] private int value;

        public CardType CardType => cardType;
        public int Value => value;
    }
}