using System;
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
        
        [SerializeField] protected List<EffectItem> _effects;
        
        [HideInInspector][SerializeField] private CardType cardType;
        [HideInInspector][SerializeField] private Sprite icon;
        [HideInInspector][SerializeField] private int value;
        [HideInInspector][SerializeField] private Sprite faceSprite;

        [Obsolete] public CardType CardType => cardType;
        [Obsolete] public int Value => value;
        [Obsolete] public Sprite FaceSprite => faceSprite;
        [Obsolete] public Sprite Icon => icon;

        public Sprite CardIcon => cardIcon;
        public bool Special => special;
        public List<EffectItem> Effects => _effects;
    }
}