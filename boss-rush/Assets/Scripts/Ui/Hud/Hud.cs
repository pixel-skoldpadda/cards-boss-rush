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
        [SerializeField] private ExchangeButton exchangeButton;
        [SerializeField] private CardsLimitContainer cardsLimitContainer;
        [SerializeField] private BossCardsContainer bossCardsContainer;
        [SerializeField] private BaseMiddleContainer deathContainer;
        [SerializeField] private DeckContainer deckContainer;
        [SerializeField] private OutContainer outContainer;
        [SerializeField] private SettingsContainer settingsContainer;
        
        public BossHealthBar BossHealthBar => bossHealthBar;
        public CardsContainer CardsContainer => cardsContainer;
        public StepContainer StepContainer => stepContainer;
        public EndTurnButton EndTurnButton => endTurnButton;
        public ExchangeButton ExchangeButton => exchangeButton;
        public CardsLimitContainer CardsLimitContainer => cardsLimitContainer;
        public BossCardsContainer BossCardsContainer => bossCardsContainer;
        public BaseMiddleContainer DeathContainer => deathContainer;
        public DeckContainer DeckContainer => deckContainer;
        public OutContainer OutContainer => outContainer;
        public SettingsContainer SettingsContainer => settingsContainer;

        public void ResetContainers()
        {
            cardsContainer.ResetContainer();
            cardsLimitContainer.ResetContainer();
            endTurnButton.ResetContainer();
            
        }
        
        public void Hide()
        {
            bossHealthBar.Hide();
            cardsContainer.Hide();
            endTurnButton.Hide();
            exchangeButton.Hide();
            cardsContainer.Hide();
            bossCardsContainer.Hide();
            cardsLimitContainer.Hide();
            deckContainer.Hide();
            outContainer.Hide();
            settingsContainer.Hide();
        }

        public void Show()
        {
            bossHealthBar.Show();
            cardsContainer.Show();
            endTurnButton.Show();
            exchangeButton.Show();
            cardsContainer.Show();
            bossCardsContainer.Show();
            cardsLimitContainer.Show();
            deckContainer.Show();
            outContainer.Show();
            settingsContainer.Show();
        }
    }
}