using System;
using Items.Card;
using UnityEngine;

namespace Items.Boss.AI
{
    [Serializable]
    public class UtilityAiAction
    {
        [SerializeField] private CardType cardType;
        [SerializeField] private AnimationCurve useActionCurve;

        public CardType CardType => cardType;
        public AnimationCurve UseActionCurve => useActionCurve;
    }
}