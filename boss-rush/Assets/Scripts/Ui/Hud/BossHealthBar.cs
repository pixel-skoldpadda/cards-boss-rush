using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private Image health;
        [SerializeField] private TextMeshProUGUI bossNameText;
        [SerializeField] private TextMeshProUGUI healthAmountText;

        private int _maxHealth;

        public void Init(int maxHealth, string bossName)
        {
            _maxHealth = maxHealth;
            bossNameText.text = bossName;
            UpdateHealthBar(maxHealth);
        }

        public void UpdateHealthBar(int currentValue)
        {
            health.fillAmount = (float) currentValue / _maxHealth;
            healthAmountText.text = $"{currentValue}/{_maxHealth}";
        }
    }
}