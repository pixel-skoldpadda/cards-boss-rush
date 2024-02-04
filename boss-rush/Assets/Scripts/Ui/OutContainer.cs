using GameObjects.Character;
using TMPro;
using Ui.Hud;
using UnityEngine;

namespace Ui
{
    public class OutContainer : BaseHudContainer
    {
        [SerializeField] private TextMeshProUGUI cardsInOutCounter;
        
        private CardsDeck _cardsDeck;
        
        public void Init(CardsDeck cardsDeck)
        {
            _cardsDeck = cardsDeck;

            _cardsDeck.OnCardGoOut += OnCardGoOut;
            _cardsDeck.OnHangUp += OnHangUp;
            _cardsDeck.OnCardsDiscarding += OnCardsDiscarding;
        }

        private void OnCardsDiscarding()
        {
            cardsInOutCounter.text = $"{_cardsDeck.OutCardsCount}";
        }

        private void OnHangUp()
        {
            cardsInOutCounter.text = $"{_cardsDeck.OutCardsCount}";
        }

        private void OnCardGoOut()
        {
            cardsInOutCounter.text = $"{_cardsDeck.OutCardsCount}";
        }

        private void OnDestroy()
        {
            _cardsDeck.OnCardGoOut -= OnCardGoOut;
            _cardsDeck.OnHangUp -= OnHangUp;
            _cardsDeck.OnCardsDiscarding -= OnCardsDiscarding;
        }
    }
}