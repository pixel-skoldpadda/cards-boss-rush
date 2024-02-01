using System.Collections.Generic;
using Items.Card;
using UnityEngine;

namespace Ui.Hud.Boss
{
    public class BossCardsContainer : BaseHudContainer 
    {
        [SerializeField] private GameObject cardIconPrefab;
        
        private readonly Dictionary<StatusType, CardIconView> _icons = new();
        
        public void AddCard(CardItem cardItem)
        {
            List<StatusItem> effects = cardItem.StatusItems;
            foreach (StatusItem effectItem in effects)
            {
                StatusType type = effectItem.Type;
                if (_icons.TryGetValue(type, out CardIconView icon))
                {
                    icon.UpdateValue(effectItem.Value);
                }
                else
                {
                    CardIconView iconView = Instantiate(cardIconPrefab, transform).GetComponent<CardIconView>();
                    iconView.Init(effectItem);

                    _icons[type] = iconView;
                }
            }
        }

        public void ClearAllCards()
        {
            foreach (CardIconView cardIcon in _icons.Values)
            {
                Destroy(cardIcon.gameObject);
            }
            _icons.Clear();
        }
    }
}