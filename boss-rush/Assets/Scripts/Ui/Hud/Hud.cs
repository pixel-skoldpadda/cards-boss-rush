using UnityEngine;

namespace Ui.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private BossHealthBar bossHealthBar;

        public BossHealthBar BossHealthBar => bossHealthBar;
    }
}