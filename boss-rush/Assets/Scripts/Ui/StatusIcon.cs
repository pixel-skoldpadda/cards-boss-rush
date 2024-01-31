using GameObjects.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class StatusIcon : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI turnCount;

        private Status _status;
        private StatusBar _statusBar;
        
        public void Init(Status status, StatusBar statusBar)
        {
            _status = status;
            _statusBar = statusBar;
            
            icon.sprite = status.Item.Icon;
            turnCount.text = $"{status.Turns}";

            _status.OnTurnsUpdated += OnTurnsUpdated;
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