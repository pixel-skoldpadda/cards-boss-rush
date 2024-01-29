using TMPro;
using UnityEngine;

namespace Ui.Hud.Card
{
    public class CardsLimitContainer : BaseHudContainer
    {
        [SerializeField] private TextMeshProUGUI limit;

        protected override void ResetContainer()
        {
            base.ResetContainer();
            
            UpdateUsedCardsCounter(0, 0);
        }

        public void UpdateUsedCardsCounter(int current, int max)
        {
            limit.text = $"{current}/{max}";
        }
    }
}