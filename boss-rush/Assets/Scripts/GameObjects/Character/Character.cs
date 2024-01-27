using System;
using Data;
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
        protected GameState gameState;
        protected CardsDeck cardsDeck;

        protected int health;
        protected int shield;

        protected Action<int> OnHealthChanged;
        protected Action<int> OnShieldChanged;

        public Action OnEndTurn { get; set; }

        protected void Construct(CharacterItem characterItem, GameState state)
        {
            item = characterItem;
            gameState = state;
            health = characterItem.MaxHealth;
            
            CreateCardsDeck();
            gameState.OnTurnStarted += OnTurnStarted;
        }

        protected abstract void OnTurnStarted();
        protected abstract void CreateCardsDeck();

        private void UseAttackCard(CardItem cardItem)
        {
            gameState.GetOpponentCharacter().TakeDamage(cardItem.Value);
        }

        private void TakeDamage(int damage)
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

        public virtual void UseCard(CardItem cardItem)
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

        public int HealthWithShield => health + shield;
        public CardsDeck CardsDeck => cardsDeck;
        public CharacterItem Item => item;


        protected int Shield
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
            OnHealthChanged = null;
            OnShieldChanged = null;
            gameState.OnTurnStarted = null;
        }
    }
}