using Items;
using Zenject;

namespace GameObjects.Character.Player
{
    public class Player : Character
    {
        [Inject]
        public void Construct(PlayerItem playerItem)
        {
            base.Construct(playerItem);
            
            healthBar.Init(playerItem.MaxHealth);
        }
        
        protected override void CreateCardsDeck()
        {
            // TODO Get cards from state
            cardsDeck = new CardsDeck(item.Deck);
        }
    }
}