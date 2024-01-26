using TMPro;
using UnityEngine;

namespace Ui.Hud
{
    public class CardsLimitContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI limit;

        public void UpdateUsedCardsCounter(int current, int max)
        {
            limit.text = $"{current}/{max}";
        }
    }
}