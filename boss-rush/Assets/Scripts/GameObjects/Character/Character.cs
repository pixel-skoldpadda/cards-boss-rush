using System;
using System.Collections.Generic;
using Data;
using Infrastructure.States;
using Items;
using Items.Card;
using Ui;
using Ui.Hud;
using UnityEngine;

namespace GameObjects.Character
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected HealthBar healthBar;
        [SerializeField] protected CharacterAnimator animator;
        [SerializeField] private StatusBar statusBar;

        //: TODO Избавить от ивента
        public Action OnEndTurn { get; set; }
        
        protected CharacterItem item;
        protected GameState gameState;
        protected CardsDeck cardsDeck;
        protected Statuses statuses;

        protected Action<int> OnHealthChanged;
        protected Action<int> OnShieldChanged;
        protected Action<int, int> OnExchangeCountChanged;

        protected int exchange;
        private int _health;
        private int _shield;

        private IGameStateMachine _stateMachine;
        
        protected void Construct(CharacterItem characterItem, GameState state, IGameStateMachine stateMachine)
        {
            item = characterItem;
            gameState = state;
            _stateMachine = stateMachine;
            _health = characterItem.MaxHealth;
            exchange = characterItem.UseExchangeLimit;

            statuses = new Statuses(this, statusBar, gameState);
            CreateCardsDeck();

            gameState.OnTurnStarted += OnTurnStarted;
        }
        
        protected abstract void CreateCardsDeck();
        protected abstract void PlayAttackAnimation();
        protected abstract void OnTurnStarted();

        public void ResetState()
        {
            cardsDeck.Reset();
            statuses.Reset();
            Shield = 0;
            Exchange = item.UseExchangeLimit;
        }
        
        public virtual void UseCard(CardItem cardItem)
        {
            List<StatusItem> statusItems = cardItem.StatusItems;
            foreach (StatusItem statusItem in statusItems)
            {
                Status status = new Status(statusItem);
                StatusSubtype subtype = statusItem.Subtype;
                
                if (StatusSubtype.Negative.Equals(subtype))
                {
                    StatusType statusType = statusItem.Type;
                    if (StatusType.Damage.Equals(statusType) || StatusType.ThroughShieldDamage.Equals(statusType))
                    {
                        PlayAttackAnimation();

                        if (status.IsInstantaneous())
                        {
                            status.Value = statuses.CalculateDamageEffect(status.Value);
                        }
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
        public Statuses Statuses => statuses;
        
        public int Health
        {
            get => _health;
            set
            {
                _health = Math.Clamp(value, 0, item.MaxHealth);
                OnHealthChanged?.Invoke(_health);
            }
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
                Hud hud = gameState.HUD;
                hud.ResetContainers();
                hud.Hide();

                _stateMachine.Enter<CheckHealthState, Character>(this);
            }
        }

        public int Shield
        {
            get => _shield;
            set
            {
                _shield = value;
                OnShieldChanged?.Invoke(value);
            }
        }

        public int Exchange
        {
            get => exchange;
            set
            {
                exchange = value;
                OnExchangeCountChanged?.Invoke(value, item.UseExchangeLimit);
            }
        }

        protected virtual void OnDestroy()
        {
            gameState.OnTurnStarted -= OnTurnStarted;
        }
    }
}