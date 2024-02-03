using System.Collections.Generic;
using Items.Card;
using UnityEngine;

namespace Ui.Hud.Boss
{
    public class BossCardsContainer : BaseHudContainer
    {
        [SerializeField] private GameObject cardIconPrefab;

        private readonly List<CardIconView> _cardIcons = new();

        public void AddCard(CardItem cardItem)
        {
            CardIconView iconView = Instantiate(cardIconPrefab, transform).GetComponent<CardIconView>();
            iconView.Init(cardItem);
            _cardIcons.Add(iconView);
        }

        public void ClearAllCards()
        {
            foreach (CardIconView cardIcon in _cardIcons)
            {
                Destroy(cardIcon.gameObject);
            }
            _cardIcons.Clear();
        }
    }
}