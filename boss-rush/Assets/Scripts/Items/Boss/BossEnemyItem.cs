using Items.Boss;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "_BOSS_ENEMY_ITEM", menuName = "Items/BossEnemy", order = 1)]
    public class BossEnemyItem : GameObjectItem
    {
        [SerializeField] private BossType type;

        public BossType Type => type;
    }
}