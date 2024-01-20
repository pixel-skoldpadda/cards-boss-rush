using Items.Boss;
using Ui.Hud;
using UnityEngine;
using Zenject;

namespace GameObjects.Enemy
{
    public class BossEnemy : MonoBehaviour
    {
        private BossHealthBar _healthBar;
        private BossEnemyItem _enemyItem;

        private int _health;
        
        [Inject]
        public void Construct(BossHealthBar healthBar, BossEnemyItem enemyItem)
        {
            _healthBar = healthBar;
            _enemyItem = enemyItem;
            _health = enemyItem.MaxHealth;

            _healthBar.Init(_health, enemyItem.ItemName);
        }
    }
}