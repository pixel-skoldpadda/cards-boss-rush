using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Boss.AI
{
    [Serializable]
    public class UtilityAiItem
    {
        [SerializeField] private int maxScore;
        [SerializeField] private List<UtilityAiAction> aiActions;

        public List<UtilityAiAction> AIActions => aiActions;
        public int MaxScore => maxScore;
    }
}