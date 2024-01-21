using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud
{
    public abstract class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image health;
        [SerializeField] private TextMeshProUGUI healthAmountText;

        private int _maxHealth;

        public void Init(int maxHealth)
        {
            _maxHealth = maxHealth;
            UpdateHealthBar(_maxHealth);
        }
        
        public void UpdateHealthBar(int currentValue)
        {
            health.fillAmount = (float) currentValue / _maxHealth;
            healthAmountText.text = $"{currentValue}/{_maxHealth}";
        }
    }
}