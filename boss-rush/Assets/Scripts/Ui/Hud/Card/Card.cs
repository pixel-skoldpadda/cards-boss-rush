using System;
using Items.Card;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui.Hud.Card
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private GameObject shirt;
        [SerializeField] private CardAnimator cardAnimator;
        [SerializeField] private EventTrigger eventTrigger;
        
        private CardItem _cardItem;
        private int _cardIndex;
        
        public Action<Card> OnCardClicked;
        
        public void Init(CardItem cardItem)
        {
            _cardItem = cardItem;
            descriptionText.text = string.Format(_cardItem.Description, _cardItem.Value);
            icon.sprite = _cardItem.CardIcon;
            
            shirt.SetActive(false);
        }

        public void OnPointerEnter()
        {
            if (!cardAnimator.IsMoving)
            {
                cardAnimator.PickUp();
            }
        }
        
        public void OnPointerExit()
        {
            if (!cardAnimator.IsMoving)
            {
                cardAnimator.PickDown();
            }
        }

        public void OnPointerClick()
        {
            if (!cardAnimator.IsMoving)
            {
                OnCardClicked?.Invoke(this);
            }
        }

        public CardAnimator CardAnimator => cardAnimator;
        public CardItem CardItem => _cardItem;

        public void ChangeInteractionEnabled(bool enabled)
        {
            eventTrigger.enabled = enabled;
        }
    }
}