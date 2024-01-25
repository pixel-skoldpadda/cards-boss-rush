using Items.Boss;
using Ui.Hud;
using UnityEngine;
using Zenject;

namespace GameObjects.Character.Enemy
{
    public class BossEnemy : Character
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [Inject]
        public void Construct(BossHealthBar bossHealthBar, BossEnemyItem enemyItem)
        {
            base.Construct(enemyItem);

            spriteRenderer.sprite = enemyItem.BossSprite;
            
            bossHealthBar.Init(health, enemyItem.ItemName);
            healthBar = bossHealthBar;
        }

        protected override void CreateCardsDeck()
        {
            cardsDeck = new CardsDeck(item.Deck, item.AttackCards, item.ProtectionCards);
        }
    }
}