using System;
using Items.Card;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui.Hud.Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private GameObject shirt;
        [SerializeField] private CardAnimator cardAnimator;
        [SerializeField] private EventTrigger eventTrigger;
        
        private CardItem _cardItem;
        private int _cardIndex;
        
        public Action<CardView> OnCardClicked;
        
        public void Init(CardItem cardItem)
        {
            _cardItem = cardItem;
            
            descriptionText.text = _cardItem.Description;
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

        public void ChangeInteractionEnabled(bool interactionEnabled)
        {
            eventTrigger.enabled = interactionEnabled;
        }
    }
}