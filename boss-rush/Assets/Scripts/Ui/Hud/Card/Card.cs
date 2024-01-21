using System;
using Items.Card;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud.Card
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image faceImage;
        [SerializeField] private GameObject shirt;
        [SerializeField] private CardAnimator cardAnimator;

        private CardItem _cardItem;
        private int _cardIndex;

        public Action<int> OnCardPickUp;
        public Action<int> OnCardPickDown;
        
        public void Init(CardItem cardItem, int cardIndex)
        {
            _cardItem = cardItem;
            _cardIndex = cardIndex;

            descriptionText.text = string.Format(_cardItem.Description, _cardItem.Value);

            faceImage.sprite = _cardItem.FaceSprite;
            shirt.SetActive(false);
        }

        public void OnPointerEnter()
        {
            cardAnimator.PickUp();
            OnCardPickUp?.Invoke(_cardIndex);
        }
        
        public void OnPointerExit()
        {
            cardAnimator.PickDown();
            OnCardPickDown.Invoke(_cardIndex);
        }

        public CardAnimator CardAnimator => cardAnimator;
    }
}