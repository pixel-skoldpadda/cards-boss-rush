using Infrastructure.Services.State;
using Items;
using Items.Card;
using Ui.Hud;
using Zenject;

namespace GameObjects.Character.Player
{
    public class Player : Character
    {
        private IGameStateService _gameStateService;
        private CardsLimitContainer _limitContainer;
        
        [Inject]
        public void Construct(PlayerItem playerItem, CardsContainer cardsContainer, CardsLimitContainer limitContainer, IGameStateService gameStateService)
        {
            base.Construct(playerItem);

            _gameStateService = gameStateService;
            _limitContainer = limitContainer;
            
            cardsContainer.InitCardsDeck(cardsDeck);
            cardsDeck.OnUsedCardsCountChanged += limitContainer.UpdateUsedCardsCounter;
            limitContainer.UpdateUsedCardsCounter(cardsDeck.CardsUsed, cardsDeck.UseCardsLimit);
            
            healthBar.Init(playerItem.MaxHealth);
            OnHealthChanged += healthBar.UpdateHealthBar;
            OnShieldChanged += healthBar.UpdateShieldCounter;
        }
        
        protected override void CreateCardsDeck()
        {
            // TODO Get cards from state
            cardsDeck = new CardsDeck(item.Deck, item.AttackCards, item.ProtectionCards, item.UseCardsLimit);
        }

        protected override void UseAttackCard(CardItem cardItem)
        {
            Character character = _gameStateService.State.GetOpponentCharacter();
            character.TakeDamage(cardItem.Value);
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