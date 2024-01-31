using Items.Card;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud.Boss
{
    public class StausIconView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI count;

        private int _currentValue;
        
        public void Init(StatusItem statusItem)
        {
            _currentValue = statusItem.Value;
            image.sprite = statusItem.Icon;
            
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