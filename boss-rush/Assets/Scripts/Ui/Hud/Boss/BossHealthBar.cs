using TMPro;
using UnityEngine;

namespace Ui.Hud.Boss
{
    public class BossHealthBar : HealthBar
    {
        [SerializeField] private TextMeshProUGUI bossNameText;

        public void Init(int maxHealth, string bossName)
        {
            Init(maxHealth);
            
            bossNameText.text = bossName;
        }
    }
}