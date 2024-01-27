using System.Collections.Generic;
using Items.Card;
using UnityEngine;

namespace Ui.Hud.Boss
{
    public class BossCardsContainer : MonoBehaviour
    {
        [SerializeField] private GameObject cardIconPrefab;
        
        private readonly Dictionary<CardType, CardIcon> _icons = new();

        public void AddCard(CardItem cardItem)
        {
            CardType type = cardItem.CardType;
            if (_icons.TryGetValue(type, out CardIcon icon))
            {
                icon.UpdateValue(cardItem.Value);
            }
            else
            {
                CardIcon cardIcon = Instantiate(cardIconPrefab, transform).GetComponent<CardIcon>();
                cardIcon.Init(cardItem);

                _icons[type] = cardIcon;
            }
        }

        public void ClearAllCards()
        {
            foreach (CardIcon cardIcon in _icons.Values)
            {
                Destroy(cardIcon.gameObject);
            }
            _icons.Clear();
        }
    }
}