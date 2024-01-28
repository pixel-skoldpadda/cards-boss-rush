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

        [Space(5)]
        [Header("A special card that the player can receive after defeating the boss")] 
        [SerializeField] private bool special;
        
        public CardType CardType => cardType;
        public int Value => value;
        public Sprite FaceSprite => faceSprite;
        public Sprite Icon => icon;
        public bool Special => special;
    }
}