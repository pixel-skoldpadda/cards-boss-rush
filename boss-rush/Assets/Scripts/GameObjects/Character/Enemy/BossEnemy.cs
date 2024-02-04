using System;
using System.Collections.Generic;
using Ai;
using Infrastructure.Services.State;
using Infrastructure.States;
using Items.Boss;
using Items.Boss.AI;
using Items.Card;
using ModestTree;
using Ui.Hud;
using Ui.Hud.Boss;
using UnityEngine;
using Action = Ai.Action;

namespace GameObjects.Character.Enemy
{
    public class BossEnemy : Character
    {
        private BossCardsContainer _cardsContainer;
        private UtilityAi _utilityAi;
        
        public void Construct(BossEnemyItem enemyItem, IGameStateService gameStateService, IGameStateMachine stateMachine)
        {
            base.Construct(enemyItem, gameStateService.State, stateMachine);

            Hud hud = gameState.HUD;
            _cardsContainer = hud.BossCardsContainer;

            BossHealthBar bossHealthBar = hud.BossHealthBar;
            bossHealthBar.Init(Health, enemyItem.ItemName);
            healthBar = bossHealthBar;
            
            OnHealthChanged += healthBar.UpdateHealthBar;
            OnShieldChanged += healthBar.UpdateShieldCounter;

            _utilityAi = new UtilityAi(enemyItem.UtilityAiItem, this, gameState.GetPlayer());
        }

        public int CalculateHealth()
        {
            List<CardItem> cardsStack = cardsDeck.CardsStack;
            if (cardsStack.IsEmpty())
            {
                return Health;
            }

            int health = Health;
            foreach (CardItem cardItem in cardsStack)
            {
                List<StatusItem> statusItems = cardItem.StatusItems;
                foreach (StatusItem statusItem in statusItems)
                {
                    if (StatusType.Health.Equals(statusItem.Type))
                    {
                        health += statusItem.Value;
                    }
                }
            } 
            return Math.Clamp(health, 0, item.MaxHealth);
        }
        
        protected override void OnTurnStarted()
        {
            if (!gameState.ActiveCharacter.IsPlayer())
            {                
                UseAllCardsInStack();
                statuses.Update();
                OnEndTurn?.Invoke();
            }
        }

        private void UseAllCardsInStack()
        {
            List<CardItem> stack = cardsDeck.CardsStack;
            cardsDeck.CardsInHand.AddRange(stack);
            
            foreach (CardItem cardItem in stack)
            {
                UseCard(cardItem);
            }
            
            cardsDeck.CardsStack.Clear();
            _cardsContainer.ClearAllCards();
        }

        private void ChooseCardsInStack()
        {
            int limit = cardsDeck.UseCardsLimit;
            for (int i = 0; i < limit; i++)
            {
                Action action = _utilityAi.ChooseBestAction();
                if (action.Score >= 0)
                {
                    UtilityAiAction actionItem = action.ActionItem;
                    CardItem cardItem = cardsDeck.ChooseCardToStack(actionItem.StatusType, actionItem.StatusSubtype);
                    _cardsContainer.AddCard(cardItem);
                }
            }
        }

        protected override void PlayAttackAnimation()
        {
            animator.PlayAttackAnimation(Vector3.left);
        }

        public override void UseCard(CardItem cardItem)
        {
            cardsDeck.UpdateUsedCardsCounter();
            
            base.UseCard(cardItem);
        }

        protected override void CreateCardsDeck()
        {
            cardsDeck = new CardsDeck(item.Deck, item.CardsOnHand, item.UseCardsLimit, statuses);
            cardsDeck.OnCardsGenerated += ChooseCardsInStack;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            OnHealthChanged -= healthBar.UpdateHealthBar;
            OnShieldChanged -= healthBar.UpdateShieldCounter;
        }
    }
}