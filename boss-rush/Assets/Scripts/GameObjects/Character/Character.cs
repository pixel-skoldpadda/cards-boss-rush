using System;
using Items;
using Items.Card;
using Ui.Hud;
using UnityEngine;

namespace GameObjects.Character
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected HealthBar healthBar;

        protected CharacterItem item;
        protected CardsDeck cardsDeck;

        protected int health;
        protected int shield;
        
        private Action onEndTurn;
        
        protected Action<int> OnHealthChanged;
        protected Action<int> OnShieldChanged;
        
        protected void Construct(CharacterItem characterItem)
        {
            item = characterItem;
            health = characterItem.MaxHealth;
            
            CreateCardsDeck();
        }

        protected abstract void CreateCardsDeck();
        protected abstract void UseAttackCard(CardItem cardItem);

        public void TakeDamage(int damage)
        {
            if (damage > Shield)
            {
                damage -= Shield;
                Shield = 0;
            }
            else
            {
                Shield -= damage;
            }
            
            Health -= damage;
            healthBar.UpdateHealthBar(health);
        }

        public void UseCard(CardItem cardItem)
        {
            CardType type = cardItem.CardType;
            if (CardType.Attack.Equals(type))
            {
                UseAttackCard(cardItem);
            }
            else if (CardType.Protection.Equals(type))
            {
                Shield += cardItem.Value;
            }
            
            cardsDeck.RemoveCardsFromHand(cardItem);
        }

        public CardsDeck CardsDeck => cardsDeck;
        public CharacterItem Item => item;

        public Action OnEndTurn
        {
            get => onEndTurn;
            set => onEndTurn = value;
        }

        public int Shield
        {
            get => shield;
            set
            {
                shield = value;
                OnShieldChanged?.Invoke(value);
            }
        }

        public int Health
        {
            get => health;
            set
            {
                health = value;
                OnHealthChanged?.Invoke(value);
            }
        }

        public virtual bool IsPlayer()
        {
            return false;
        }

        protected virtual void OnDestroy()
        {
            OnHealthChanged -= healthBar.UpdateHealthBar;
            OnShieldChanged -= healthBar.UpdateShieldCounter;
        }
    }
}