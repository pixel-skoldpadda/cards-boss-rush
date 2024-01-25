using Items;
using Ui.Hud;
using Zenject;

namespace GameObjects.Character.Player
{
    public class Player : Character
    {
        private CardsContainer _cardsContainer;
        
        [Inject]
        public void Construct(PlayerItem playerItem, CardsContainer cardsContainer)
        {
            base.Construct(playerItem);

            _cardsContainer = cardsContainer;
            healthBar.Init(playerItem.MaxHealth);

            cardsDeck.OnCardsGenerated += OnCardsGenerated;
            cardsDeck.OnHangUp += OnHangUp;
            
            cardsDeck.GeneratedCardsInHand();
        }

        private void OnCardsGenerated()
        {
            _cardsContainer.AddCardsToDeck(cardsDeck.CardsInHand);
            _cardsContainer.UpdateCardsInDeckCounter(cardsDeck.GetCardsCount());
        }

        private void OnHangUp()
        {
            
        }

        protected override void CreateCardsDeck()
        {
            // TODO Get cards from state
            cardsDeck = new CardsDeck(item.Deck, item.AttackCards, item.ProtectionCards);
        }
    }
}