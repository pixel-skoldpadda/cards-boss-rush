using Infrastructure.Services.State;
using Items;
using Items.Card;
using Ui.Hud;
using Zenject;

namespace GameObjects.Character.Player
{
    public class Player : Character
    {
        private CardsLimitContainer _limitContainer;
        
        [Inject]
        public void Construct(PlayerItem playerItem, IGameStateService gameStateService)
        {
            base.Construct(playerItem, gameStateService.State);

            Hud hud = gameState.HUD;

            _limitContainer = hud.CardsLimitContainer;
            
            hud.CardsContainer.InitCardsDeck(cardsDeck);
            cardsDeck.OnUsedCardsCountChanged += _limitContainer.UpdateUsedCardsCounter;
            _limitContainer.UpdateUsedCardsCounter(cardsDeck.CardsUsed, cardsDeck.UseCardsLimit);
            
            healthBar.Init(playerItem.MaxHealth);
            OnHealthChanged += healthBar.UpdateHealthBar;
            OnShieldChanged += healthBar.UpdateShieldCounter;
        }

        protected override void OnTurnStarted()
        {
            if (gameState.ActiveCharacter.IsPlayer())
            {
                Shield = 0;
            }
        }

        protected override void CreateCardsDeck()
        {
            // TODO Get cards from state
            cardsDeck = new CardsDeck(item.Deck, item.AttackCards, item.ProtectionCards, item.UseCardsLimit);
        }

        public override bool IsPlayer()
        {
            return true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            cardsDeck.OnUsedCardsCountChanged -= _limitContainer.UpdateUsedCardsCounter;
        }
    }
}