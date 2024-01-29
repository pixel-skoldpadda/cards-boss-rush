using System.ComponentModel;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud
{
    public abstract class HealthBar : BaseHudContainer
    {
        [Description("Progress colors")]
        [SerializeField] private Color normalColor;
        [SerializeField] private Color shieldColor;
        
        [Space(10)]
        [SerializeField] private Image health;
        [SerializeField] private TextMeshProUGUI healthAmountText;
        [SerializeField] private TextMeshProUGUI shieldCountText;
        [SerializeField] private Image shield;
        
        private int _maxHealth;

        private Tweener _fillTween;
        private Sequence _shieldSequence;
        
        public void Init(int maxHealth)
        {
            _maxHealth = maxHealth;
            UpdateHealthBar(_maxHealth);
        }
        
        public void UpdateHealthBar(int currentValue)
        {
            _fillTween = health.DOFillAmount((float) currentValue / _maxHealth, .5f);
            healthAmountText.text = $"{currentValue}/{_maxHealth}";
        }

        public void UpdateShieldCounter(int shieldCount)
        {
            shieldCountText.text = $"{shieldCount}";

            _shieldSequence = DOTween.Sequence()
                .Append(shield.DOColor(shieldCount > 0 ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0), .5f))
                .Join(shield.transform.DOScale(shieldCount > 0 ? Vector3.one : Vector3.zero, .5f))
                .Join(health.DOColor(shieldCount > 0 ? shieldColor : normalColor, .5f));
        }

        private void OnDestroy()
        {
            _fillTween?.Kill();
            _fillTween = null;
            
            _shieldSequence?.Kill();
            _shieldSequence = null;
        }
    }
}