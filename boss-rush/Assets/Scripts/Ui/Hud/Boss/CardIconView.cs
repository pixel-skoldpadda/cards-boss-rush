using Items.Card;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Hud.Boss
{
    public class CardIconView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Hint hint;
        
        public void Init(CardItem cardItem)
        {
            image.sprite = cardItem.CardIcon;
            hint.Init(cardItem.Description);
        }

        public void OnPointerEnter()
        {
            hint.Show();
        }

        public void OnPointerExit()
        {
            hint.Hide();
        }
    }
}