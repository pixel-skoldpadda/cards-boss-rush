using System.Collections.Generic;
using Items.Card;
using UnityEngine;

namespace Ui.Hud.Boss
{
    public class BossCardsContainer : BaseHudContainer 
    {
        [SerializeField] private GameObject cardIconPrefab;
        
        private readonly Dictionary<EffectType, EffectIconView> _icons = new();
        
        public void AddCard(CardItem cardItem)
        {
            List<EffectItem> effects = cardItem.Effects;
            foreach (EffectItem effectItem in effects)
            {
                EffectType type = effectItem.Type;
                if (_icons.TryGetValue(type, out EffectIconView icon))
                {
                    icon.UpdateValue(effectItem.Value);
                }
                else
                {
                    EffectIconView effectIconView = Instantiate(cardIconPrefab, transform).GetComponent<EffectIconView>();
                    effectIconView.Init(effectItem);

                    _icons[type] = effectIconView;
                }
            }
        }

        public void ClearAllCards()
        {
            foreach (EffectIconView cardIcon in _icons.Values)
            {
                Destroy(cardIcon.gameObject);
            }
            _icons.Clear();
        }
    }
}