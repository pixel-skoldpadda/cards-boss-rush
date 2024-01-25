using UnityEngine;

namespace Items.Boss
{
    [CreateAssetMenu(fileName = "_BOSS_ENEMY_ITEM", menuName = "Items/BossEnemy", order = 1)]
    public class BossEnemyItem : CharacterItem
    {
        [SerializeField] private BossType type;
        [SerializeField] private Sprite bossSprite;

        public BossType Type => type;
        public Sprite BossSprite => bossSprite;
    }
}