using GameObjects.Character;
using TMPro;
using UnityEngine;

namespace Ui.Hud
{
    public class DeckContainer : BaseHudContainer
    {
        [SerializeField] private TextMeshProUGUI cardsInDeckCounter;

        private CardsDeck _cardsDeck;
        
        public void Init(CardsDeck cardsDeck)
        {
            _cardsDeck = cardsDeck;

            _cardsDeck.OnCardsGenerated += OnCardsGenerated;
            _cardsDeck.OnHangUp += OnHangUp;
        }

        private void OnHangUp()
        {
            cardsInDeckCounter.text = $"{_cardsDeck.CardsCount}";
        }

        private void OnCardsGenerated()
        {
            cardsInDeckCounter.text = $"{_cardsDeck.CardsCount}";
        }

        private void OnDestroy()
        {
            _cardsDeck.OnCardsGenerated -= OnCardsGenerated;
            _cardsDeck.OnHangUp -= OnHangUp;
        }
    }
}