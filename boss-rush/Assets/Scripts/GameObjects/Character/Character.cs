using System;
using System.Collections.Generic;
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
        
        //: TODO Избавить от ивента
        public Action OnEndTurn { get; set; }
        
        protected CharacterItem item;
        protected GameState gameState;
        protected CardsDeck cardsDeck;

        protected Action<int> OnHealthChanged;
        protected Action<int> OnShieldChanged;

        private Statuses statuses;
        
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
            statuses = new Statuses(this);
        }
        
        protected abstract void CreateCardsDeck();
        public abstract void PlayAttackAnimation();

        public virtual void UseCard(CardItem cardItem)
        {
            List<StatusItem> statusItems = cardItem.StatusItems;
            foreach (StatusItem status in statusItems)
            {
                StatusSubtype subtype = status.Subtype;
                if (StatusSubtype.Negative.Equals(subtype))
                {
                    StatusType statusType = status.Type;
                    if (StatusType.Damage.Equals(statusType) || StatusType.ThroughShieldDamage.Equals(statusType))
                    {
                        PlayAttackAnimation();
                    }
                    
                    gameState.GetOpponentCharacter().statuses.AddStatus(status);
                }
                else if (StatusSubtype.Positive.Equals(subtype))
                {
                    statuses.AddStatus(status);
                }
            }
        }

        public CardsDeck CardsDeck => cardsDeck;
        public CharacterItem Item => item;
        public CharacterAnimator Animator => animator;
        
        public int Health
        {
            get => health;
            set
            {
                health = Math.Clamp(value, 0, item.MaxHealth);
                OnHealthChanged?.Invoke(health);
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

        public void TakeDamage(int damage, bool throughShield = false)
        {
            if (Health <= 0)
            {
                return;
            }

            if (!throughShield)
            {
                damage -= Shield;
                Shield = damage < 0 ? Math.Abs(damage) : 0;
            }

            if (damage < 0)
            {
                return;
            }
            
            Health -= damage;
                
            animator.PlayDamageAnimation();
            healthBar.UpdateHealthBar(Health);
            if (Health <= 0)
            {
                gameState.HUD.Hide();
                _stateMachine.Enter<CheckHealthState>();
            }
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
    }
}