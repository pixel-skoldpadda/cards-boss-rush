using System.Collections.Generic;
using Ai;
using Infrastructure.Services.State;
using Items.Boss;
using Items.Card;
using Ui.Hud;
using Ui.Hud.Boss;
using UnityEngine;
using Zenject;

namespace GameObjects.Character.Enemy
{
    public class BossEnemy : Character
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private BossCardsContainer _cardsContainer;
        private UtilityAi _utilityAi;
        
        [Inject]
        public void Construct(BossEnemyItem enemyItem, IGameStateService gameStateService)
        {
            base.Construct(enemyItem, gameStateService.State);

            Hud hud = gameState.HUD;
            _cardsContainer = hud.BossCardsContainer;
            
            spriteRenderer.sprite = enemyItem.BossSprite;

            BossHealthBar bossHealthBar = hud.BossHealthBar;
            bossHealthBar.Init(health, enemyItem.ItemName);
            healthBar = bossHealthBar;
            
            OnHealthChanged += healthBar.UpdateHealthBar;
            OnShieldChanged += healthBar.UpdateShieldCounter;

            _utilityAi = new UtilityAi(enemyItem.UtilityAiItem, this, gameState.GetPlayer());
        }

        protected override void OnTurnStarted()
        {
            if (gameState.ActiveCharacter.IsPlayer())
            {
                ChooseCardsInStack();
            }
            else
            {
                Shield = 0;
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

        public override void UseCard(CardItem cardItem)
        {
            cardsDeck.UpdateUsedCardsCounter();
            
            base.UseCard(cardItem);
        }

        protected override void CreateCardsDeck()
        {
            cardsDeck = new CardsDeck(item.Deck, item.AttackCards, item.ProtectionCards, item.UseCardsLimit);
        }
    }
}