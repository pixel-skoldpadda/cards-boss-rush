using System.Collections.Generic;
using Items.Card;
using UnityEngine;

namespace Ui.Hud.Boss
{
    public class BossStatusContainer : BaseHudContainer 
    {
        [SerializeField] private GameObject cardIconPrefab;
        
        private readonly Dictionary<StatusType, StausIconView> _icons = new();
        
        public void AddCard(CardItem cardItem)
        {
            List<StatusItem> effects = cardItem.StatusItems;
            foreach (StatusItem effectItem in effects)
            {
                StatusType type = effectItem.Type;
                if (_icons.TryGetValue(type, out StausIconView icon))
                {
                    icon.UpdateValue(effectItem.Value);
                }
                else
                {
                    StausIconView stausIconView = Instantiate(cardIconPrefab, transform).GetComponent<StausIconView>();
                    stausIconView.Init(effectItem);

                    _icons[type] = stausIconView;
                }
            }
        }

        public void ClearAllCards()
        {
            foreach (StausIconView cardIcon in _icons.Values)
            {
                Destroy(cardIcon.gameObject);
            }
            _icons.Clear();
        }
    }
}