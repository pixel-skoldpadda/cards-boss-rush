using UnityEngine;

namespace Items.Card
{
    [CreateAssetMenu(fileName = "CARD_ITEM", menuName = "Items/Card", order = 0)]
    public class CardItem : Item
    {
        [SerializeField] private CardType cardType;
        [SerializeField] private Sprite icon;
        [SerializeField] private int value;
        [SerializeField] private Sprite faceSprite;
        
        public CardType CardType => cardType;
        public int Value => value;
        public Sprite FaceSprite => faceSprite;
        public Sprite Icon => icon;
    }
}