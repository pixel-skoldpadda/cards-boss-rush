using UnityEngine;

namespace Ui.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private BossHealthBar bossHealthBar;
        [SerializeField] private CardsContainer cardsContainer;

        public BossHealthBar BossHealthBar => bossHealthBar;
        public CardsContainer CardsContainer => cardsContainer;
    }
}