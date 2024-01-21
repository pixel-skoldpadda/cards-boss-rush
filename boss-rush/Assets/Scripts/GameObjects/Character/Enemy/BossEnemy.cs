using Items.Boss;
using Ui.Hud;
using Zenject;

namespace GameObjects.Character.Enemy
{
    public class BossEnemy : Character
    {
        [Inject]
        public void Construct(BossHealthBar bossHealthBar, BossEnemyItem enemyItem)
        {
            base.Construct(enemyItem);
            
            bossHealthBar.Init(health, enemyItem.ItemName);
            healthBar = bossHealthBar;
        }

        protected override void CreateCardsDeck()
        {
            cardsDeck = new CardsDeck(item.Deck, item.AttackCards, item.ProtectionCards);
        }
    }
}