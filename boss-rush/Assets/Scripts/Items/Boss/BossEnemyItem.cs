using System.ComponentModel;
using Items.Boss.AI;
using UnityEngine;

namespace Items.Boss
{
    [CreateAssetMenu(fileName = "_BOSS_ENEMY_ITEM", menuName = "Items/BossEnemy", order = 1)]
    public class BossEnemyItem : CharacterItem
    {
        [SerializeField] private BossType type;
        
        [Space(10)] 
        [Description("AI settings")]
        [SerializeField]
        private UtilityAiItem utilityAiItem;
        
        public BossType Type => type;
        public UtilityAiItem UtilityAiItem => utilityAiItem;
    }
}