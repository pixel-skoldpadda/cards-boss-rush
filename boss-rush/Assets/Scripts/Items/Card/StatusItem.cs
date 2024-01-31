using System;
using UnityEngine;

namespace Items.Card
{
    [Serializable]
    public class StatusItem
    {
        [SerializeField] private StatusType type;
        [SerializeField] private StatusSubtype subtype;
        
        [Space(15)]
        [SerializeField] private int value;
        [SerializeField] private int turns;
        [SerializeField] private Sprite icon;

        public StatusType Type => type;
        public StatusSubtype Subtype => subtype;

        public int Turns => turns;
        public int Value => value;
        public Sprite Icon => icon;
    }
}