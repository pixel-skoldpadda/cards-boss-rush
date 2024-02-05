using System.Collections.Generic;
using GameObjects.Character;
using UnityEngine;

namespace Ui
{
    public class StatusBar : MonoBehaviour
    {
        [SerializeField] private GameObject statusIconPrefab;

        private readonly List<StatusIcon> _statusIcons = new();
        
        public void AddStatusIcon(Status status)
        {
            StatusIcon statusIcon = Instantiate(statusIconPrefab, transform).GetComponent<StatusIcon>();
            statusIcon.Init(status, this);
            _statusIcons.Add(statusIcon);
        }
        
        public void RemoveStatusIcon(StatusIcon statusIcon)
        {
            _statusIcons.Remove(statusIcon);
            Destroy(statusIcon.gameObject);
        }

        public void RemoveStatusIconById(string id)
        {
            foreach (StatusIcon statusIcon in _statusIcons)
            {
                if (id.Equals(statusIcon.Status.ID))
                {
                    RemoveStatusIcon(statusIcon);
                    break;
                }
            }
        }
        
        public void RemoveAllIcons()
        {
            foreach (StatusIcon statusIcon in _statusIcons)
            {
                Destroy(statusIcon.gameObject);
            }
            _statusIcons.Clear();
        }
    }
}