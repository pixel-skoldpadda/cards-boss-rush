using System;
using Data;
using Infrastructure.States;
using Items;
using Items.Card;
using Ui.Hud;
using UnityEngine;

namespace GameObjects.Character
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected HealthBar healthBar;
        [SerializeField] protected CharacterAnimator animator;

        public Action OnEndTurn { get; set; }
        
        protected CharacterItem item;
        protected GameState gameState;
        protected CardsDeck cardsDeck;

        protected Action<int> OnHealthChanged;
        protected Action<int> OnShieldChanged;

        private int health;
        private int shield;

        private IGameStateMachine _stateMachine;
        
        protected void Construct(CharacterItem characterItem, GameState state, IGameStateMachine stateMachine)
        {
            item = characterItem;
            gameState = state;
            _stateMachine = stateMachine;
            health = characterItem.MaxHealth;
            
            CreateCardsDeck();
        }
        
        protected abstract void CreateCardsDeck();
        protected abstract void UseAttackCard(CardItem cardItem);

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

        public CardsDeck CardsDeck => cardsDeck;
        public CharacterItem Item => item;
        public CharacterAnimator Animator => animator;

        public int Health
        {
            get => health;
            set
            {
                health = value;
                OnHealthChanged?.Invoke(value);
            }
        }

        public void ResetShield()
        {
            Shield = 0;
        }
        
        public virtual bool IsPlayer()
        {
            return false;
        }

        protected int Shield
        {
            get => shield;
            set
            {
                shield = value;
                OnShieldChanged?.Invoke(value);
            }
        }
        
        protected virtual void OnDestroy()
        {
            OnHealthChanged = null;
            OnShieldChanged = null;
            gameState.OnTurnStarted = null;
        }

        public void TakeDamage(int damage)
        {
            damage -= Shield;
            Shield = damage < 0 ? Math.Abs(damage) : 0;

            if (damage < 0)
            {
                Shield = Math.Abs(damage);
            }
            else
            {
                Shield = 0;
                Health -= damage;
                
                animator.PlayDamageAnimation();
                
                healthBar.UpdateHealthBar(Health);
                if (Health <= 0)
                {
                    _stateMachine.Enter<CheckHealthState>();
                }
            }
        }
    }
}