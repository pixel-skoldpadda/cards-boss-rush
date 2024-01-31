﻿using System.Collections.Generic;
using Infrastructure.Services.State;
using Infrastructure.States;
using Items;
using Items.Card;
using Ui.Hud;
using Ui.Hud.Card;
using UnityEngine;
using Zenject;

namespace GameObjects.Character.Player
{
    public class Player : Character
    {
        private CardsLimitContainer _limitContainer;
        
        [Inject]
        public void Construct(PlayerItem playerItem, IGameStateService gameStateService, IGameStateMachine stateMachine)
        {
            base.Construct(playerItem, gameStateService.State, stateMachine);

            Hud hud = gameState.HUD;

            _limitContainer = hud.CardsLimitContainer;
            
            hud.CardsContainer.InitCardsDeck(cardsDeck);
            cardsDeck.OnUsedCardsCountChanged += _limitContainer.UpdateUsedCardsCounter;
            _limitContainer.UpdateUsedCardsCounter(cardsDeck.CardsUsed, cardsDeck.UseCardsLimit);
            
            healthBar.Init(playerItem.MaxHealth);
            OnHealthChanged += healthBar.UpdateHealthBar;
            OnShieldChanged += healthBar.UpdateShieldCounter;
        }

        protected override void CreateCardsDeck()
        {
            List<CardItem> cardItems = item.Deck;
            cardItems.AddRange(gameState.PlayerCards);

            cardsDeck = new CardsDeck(cardItems, item.CardsOnHand, item.UseCardsLimit);
        }

        protected override void PlayAttackAnimation()
        {
            animator.PlayAttackAnimation(Vector3.right);
        }

        protected override void OnTurnStarted()
        {
            if (gameState.ActiveCharacter.IsPlayer())
            {
                statuses.Update();
            }
        }

        public override bool IsPlayer()
        {
            return true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            cardsDeck.OnUsedCardsCountChanged -= _limitContainer.UpdateUsedCardsCounter;
            OnHealthChanged -= healthBar.UpdateHealthBar;
            OnShieldChanged -= healthBar.UpdateShieldCounter;
        }
    }
}