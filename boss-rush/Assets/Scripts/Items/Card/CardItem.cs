using System.Collections.Generic;
using UnityEngine;

namespace Items.Card
{
    [CreateAssetMenu(fileName = "CARD_ITEM", menuName = "Items/Card", order = 0)]
    public class CardItem : Item
    {
        [SerializeField] private Sprite cardIcon;
        [Header("A special card that the player can receive after defeating the boss")] 
        [SerializeField] private bool special;
        [SerializeField] protected List<StatusItem> statusItems;

        public Sprite CardIcon => cardIcon;
        public bool Special => special;
        public List<StatusItem> StatusItems => statusItems;
    }
}