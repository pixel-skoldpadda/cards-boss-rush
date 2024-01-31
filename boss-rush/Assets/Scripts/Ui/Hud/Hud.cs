using Ui.Hud.Boss;
using Ui.Hud.Card;
using Ui.Hud.MiddleContainers;
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
        [SerializeField] private BossCardsContainer bossCardsContainer;
        [SerializeField] private BaseMiddleContainer deathContainer;
        
        public BossHealthBar BossHealthBar => bossHealthBar;
        public CardsContainer CardsContainer => cardsContainer;
        public StepContainer StepContainer => stepContainer;
        public EndTurnButton EndTurnButton => endTurnButton;
        public CardsLimitContainer CardsLimitContainer => cardsLimitContainer;
        public BossCardsContainer BossCardsContainer => bossCardsContainer;
        public BaseMiddleContainer DeathContainer => deathContainer;

        public void Hide()
        {
            bossHealthBar.Hide();
            CardsContainer.Hide();
            EndTurnButton.Hide();
            CardsContainer.Hide();
            BossCardsContainer.Hide();
            cardsLimitContainer.Hide();
        }

        public void Show()
        {
            bossHealthBar.Show();
            CardsContainer.Show();
            EndTurnButton.Show();
            CardsContainer.Show();
            BossCardsContainer.Show();
            cardsLimitContainer.Show();
        }
    }
}