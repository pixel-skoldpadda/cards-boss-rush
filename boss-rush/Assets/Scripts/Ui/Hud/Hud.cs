using UnityEngine;

namespace Ui.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private BossHealthBar bossHealthBar;
        [SerializeField] private CardsContainer cardsContainer;
        [SerializeField] private StepContainer stepContainer;
        [SerializeField] private EndTurnButton endTurnButton;
        [SerializeField] private CardsLimitContainer cardsLimitContainer;
        
        public BossHealthBar BossHealthBar => bossHealthBar;
        public CardsContainer CardsContainer => cardsContainer;
        public StepContainer StepContainer => stepContainer;
        public EndTurnButton EndTurnButton => endTurnButton;
        public CardsLimitContainer CardsLimitContainer => cardsLimitContainer;
    }
}