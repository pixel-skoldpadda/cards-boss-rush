﻿using UnityEngine;

namespace Items.Boss
{
    [CreateAssetMenu(fileName = "_BOSS_ENEMY_ITEM", menuName = "Items/BossEnemy", order = 1)]
    public class BossEnemyItem : CharacterItem
    {
        [SerializeField] private BossType type;

        public BossType Type => type;
    }
}