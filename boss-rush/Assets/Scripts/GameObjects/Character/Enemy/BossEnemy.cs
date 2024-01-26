using Items.Boss;
using Items.Card;
using Ui.Hud;
using UnityEngine;
using Zenject;

namespace GameObjects.Character.Enemy
{
    public class BossEnemy : Character
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Player.Player _player;
        
        [Inject]
        public void Construct(BossHealthBar bossHealthBar, BossEnemyItem enemyItem, Player.Player player)
        {
            base.Construct(enemyItem);

            _player = player;
            
            spriteRenderer.sprite = enemyItem.BossSprite;
            
            bossHealthBar.Init(health, enemyItem.ItemName);
            healthBar = bossHealthBar;
            
            OnHealthChanged += healthBar.UpdateHealthBar;
            OnShieldChanged += healthBar.UpdateShieldCounter;
        }

        protected override void CreateCardsDeck()
        {
            cardsDeck = new CardsDeck(item.Deck, item.AttackCards, item.ProtectionCards, item.UseCardsLimit);
        }

        protected override void UseAttackCard(CardItem cardItem)
        {
            _player.TakeDamage(cardItem.Value);
        }
    }
}