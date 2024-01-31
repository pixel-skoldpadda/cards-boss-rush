using System;
using Items.Card;
using UnityEngine;

namespace Items.Boss.AI
{
    [Serializable]
    public class UtilityAiAction
    {
        [SerializeField] private StatusType statusType;
        [SerializeField] private StatusSubtype statusSubtype;
        [SerializeField] private AnimationCurve useActionCurve;
        
        public AnimationCurve UseActionCurve => useActionCurve;
        public StatusType StatusType => statusType;
        public StatusSubtype StatusSubtype => statusSubtype;
    }
}