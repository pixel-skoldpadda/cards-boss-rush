using Ui.Hud.Boss;
using Ui.Hud.Card;
using Ui.Hud.MiddleContainers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private BossHealthBar bossHealthBar;
        [SerializeField] private CardsContainer cardsContainer;
        [SerializeField] private StepContainer stepContainer;
        [SerializeField] private EndTurnButton endTurnButton;
        [SerializeField] private CardsLimitContainer cardsLimitContainer;
        [FormerlySerializedAs("bossCardsContainer")] [SerializeField] private BossStatusContainer bossStatusContainer;
        [SerializeField] private BaseMiddleContainer deathContainer;
        
        public BossHealthBar BossHealthBar => bossHealthBar;
        public CardsContainer CardsContainer => cardsContainer;
        public StepContainer StepContainer => stepContainer;
        public EndTurnButton EndTurnButton => endTurnButton;
        public CardsLimitContainer CardsLimitContainer => cardsLimitContainer;
        public BossStatusContainer BossStatusContainer => bossStatusContainer;
        public BaseMiddleContainer DeathContainer => deathContainer;

        public void Hide()
        {
            bossHealthBar.Hide();
            CardsContainer.Hide();
            EndTurnButton.Hide();
            CardsContainer.Hide();
            BossStatusContainer.Hide();
            cardsLimitContainer.Hide();
        }

        public void Show()
        {
            bossHealthBar.Show();
            CardsContainer.Show();
            EndTurnButton.Show();
            CardsContainer.Show();
            BossStatusContainer.Show();
            cardsLimitContainer.Show();
        }
    }
}