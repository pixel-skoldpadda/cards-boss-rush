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

        [Space(10)]
        [Header("Logs the calculation of action scores.")]
        [SerializeField] private bool actionsLogging;
        
        public List<UtilityAiAction> AIActions => aiActions;
        public int MaxScore => maxScore;
        public bool ActionsLogging => actionsLogging;
    }
}