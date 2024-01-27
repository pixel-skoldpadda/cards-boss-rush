using Items.Card;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud.Boss
{
    public class CardIcon : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI count;

        private int _currentValue;
        
        public void Init(CardItem cardItem)
        {
            _currentValue = cardItem.Value;
            
            image.sprite = cardItem.Icon;
            UpdateCounterText();
        }

        public void UpdateValue(int value)
        {
            _currentValue += value;
            UpdateCounterText();
        }

        private void UpdateCounterText()
        {
            count.text = $"{_currentValue}";
        }
    }
}