using GameObjects.Character;
using Items.Card;
using TMPro;
using Ui.Hud;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class StatusIcon : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI turnCount;
        [SerializeField] private Hint hint;

        private Status _status;
        private StatusBar _statusBar;
        
        public void Init(Status status, StatusBar statusBar)
        {
            _status = status;
            _statusBar = statusBar;

            StatusItem item = status.Item;
            icon.sprite = item.Icon;
            turnCount.text = $"{status.Turns}";
            
            hint.Init(item.Description);

            _status.OnTurnsUpdated += OnTurnsUpdated;
        }

        public void OnPointerEnter()
        {
            hint.Show();
        }

        public void OnPointerExit()
        {
            hint.Hide();
        }
        
        private void OnTurnsUpdated(int turns)
        {
            turnCount.text = $"{turns}";

            if (turns == 0)
            {
                _statusBar.RemoveStatusIcon(this);
            }
        }

        private void OnDestroy()
        {
            _status.OnTurnsUpdated -= OnTurnsUpdated;
        }
    }
}