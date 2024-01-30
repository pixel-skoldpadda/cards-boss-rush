using System;
using UnityEngine;

namespace Items.Card
{
    [Serializable]
    public class EffectItem
    {
        [SerializeField] private EffectType type;
        [SerializeField] private int value;
        [SerializeField] private int turns;
        [SerializeField] private Sprite icon;

        public EffectType Type => type;
        public int Turns => turns;
        public int Value => value;
        public Sprite Icon => icon;
    }
}