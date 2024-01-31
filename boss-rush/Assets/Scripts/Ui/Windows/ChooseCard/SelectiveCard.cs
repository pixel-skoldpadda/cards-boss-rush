using System.Collections.Generic;
using DG.Tweening;
using Items.Card;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui.Windows.ChooseCard
{
    public class SelectiveCard : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private EventTrigger eventTrigger;
        [SerializeField] private RectTransform rectTransform;

        private CardItem _cardItem;
        private ChooseCardWindow _window;
        
        private Tweener _scalePositiveTween;
        private Tweener _scaleNegativeTween;
        
        public void Init(CardItem cardItem, ChooseCardWindow window)
        {
            _cardItem = cardItem;
            _window = window;

            icon.sprite = cardItem.CardIcon;

            List<StatusItem> statusItems = _cardItem.StatusItems;
            
            object[] values = new object[statusItems.Count];
            for (var i = 0; i < statusItems.Count; i++)
            {
                values[i] = statusItems[i].Value;
            }
            
            description.text = string.Format(cardItem.Description, values);
        }

        public void OnPointerEnter()
        {
            if (_scaleNegativeTween != null || _scaleNegativeTween.IsActive())
            {
                _scaleNegativeTween.Kill();
            }
            
            if (_scalePositiveTween == null || !_scalePositiveTween.IsActive())
            {
                _scalePositiveTween = rectTransform.DOScale(new Vector3(1.15f, 1.15f, 1), .25f).
                    OnKill(() => rectTransform.localScale = new Vector3(1.15f, 1.15f, 1));   
            }
        }

        public void OnPointerExit()
        {
            if (_scalePositiveTween != null || _scalePositiveTween.IsActive())
            {
                _scalePositiveTween.Kill();
            }
            
            if (_scaleNegativeTween == null || !_scaleNegativeTween.IsActive())
            {
                _scaleNegativeTween = rectTransform.DOScale(Vector3.one, .25f).
                    OnKill(() => rectTransform.localScale = Vector3.one);   
            }
        }
        
        public void OnPointerClick()
        {
            DisableInteraction();
            _window.OnCardClicked(_cardItem);
        }

        public void DisableInteraction()
        {
            eventTrigger.enabled = false;
        }
    }
}