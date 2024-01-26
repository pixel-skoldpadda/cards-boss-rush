using System.ComponentModel;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud
{
    public abstract class HealthBar : MonoBehaviour
    {
        [Description("Progress colors")]
        [SerializeField] private Color normalColor;
        [SerializeField] private Color shieldColor;
        
        [Space(10)]
        [SerializeField] private Image health;
        [SerializeField] private TextMeshProUGUI healthAmountText;
        [SerializeField] private TextMeshProUGUI shieldCountText;
        [SerializeField] private GameObject shield;
        
        private int _maxHealth;

        public void Init(int maxHealth)
        {
            _maxHealth = maxHealth;
            UpdateHealthBar(_maxHealth);
        }
        
        public void UpdateHealthBar(int currentValue)
        {
            health.DOFillAmount((float)currentValue / _maxHealth, .5f);
            healthAmountText.text = $"{currentValue}/{_maxHealth}";
        }

        public void UpdateShieldCounter(int shieldCount)
        {
            shieldCountText.text = $"{shieldCount}";
            shield.SetActive(shieldCount > 0);
            health.color = shieldCount > 0 ? shieldColor : normalColor;
        }
    }
}