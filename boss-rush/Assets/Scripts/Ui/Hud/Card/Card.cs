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
        
        public Action<Card> OnCardClicked;
        
        public void Init(CardItem cardItem)
        {
            _cardItem = cardItem;
            descriptionText.text = string.Format(_cardItem.Description, _cardItem.Value);

            faceImage.sprite = _cardItem.FaceSprite;
            shirt.SetActive(false);
        }

        public void OnPointerEnter()
        {
            if (!cardAnimator.IsMoving())
            {
                cardAnimator.PickUp();
            }
        }
        
        public void OnPointerExit()
        {
            if (!cardAnimator.IsMoving())
            {
                cardAnimator.PickDown();
            }
        }

        public void OnPointerClick()
        {
            if (!cardAnimator.IsMoving())
            {
                OnCardClicked?.Invoke(this);
            }
        }
        
        public CardAnimator CardAnimator => cardAnimator;
    }
}