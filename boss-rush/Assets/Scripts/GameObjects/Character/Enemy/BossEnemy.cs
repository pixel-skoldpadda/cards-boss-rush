using System.Collections.Generic;
using Ai;
using Infrastructure.Services.State;
using Infrastructure.States;
using Items.Boss;
using Items.Card;
using Ui.Hud;
using Ui.Hud.Boss;
using UnityEngine;

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
            
            gameState.OnTurnStarted += OnTurnStarted;
        }

        private void OnTurnStarted()
        {
            if (!gameState.ActiveCharacter.IsPlayer())
            {                
                UseAllCardsInStack();
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

            OnEndTurn?.Invoke();
        }

        private void ChooseCardsInStack()
        {
            int limit = cardsDeck.UseCardsLimit;
            for (int i = 0; i < limit; i++)
            {
                Action action = _utilityAi.ChooseBestAction();
                if (action.Score >= 0)
                {
                    CardItem cardItem = cardsDeck.ChooseCardToStack(action.CardType);
                    _cardsContainer.AddCard(cardItem);
                }
            }
        }

        protected override void UseAttackCard(CardItem cardItem)
        {
            animator.PlayAttackAnimation(Vector3.left);
            gameState.GetOpponentCharacter().TakeDamage(cardItem.Value);
        }

        public override void UseCard(CardItem cardItem)
        {
            cardsDeck.UpdateUsedCardsCounter();
            
            base.UseCard(cardItem);
        }

        protected override void CreateCardsDeck()
        {
            cardsDeck = new CardsDeck(item.Deck, item.AttackCards, item.ProtectionCards, item.UseCardsLimit);
            cardsDeck.OnCardsGenerated += ChooseCardsInStack;
        }

        protected void OnDestroy()
        {
            gameState.OnTurnStarted -= OnTurnStarted;
            OnHealthChanged -= healthBar.UpdateHealthBar;
            OnShieldChanged -= healthBar.UpdateShieldCounter;
        }
    }
}