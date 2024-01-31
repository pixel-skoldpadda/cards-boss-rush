using System.Collections.Generic;
using Items.Card;
using Ui;

namespace GameObjects.Character
{
    public class Statuses
    {
        private readonly Character _character;
        private readonly StatusBar _statusBar;

        private readonly Dictionary<StatusType, Status> _currentStatuses = new();
        
        public Statuses(Character character, StatusBar statusBar)
        {
            _character = character;
            _statusBar = statusBar;
        }

        public void AddStatus(StatusItem statusItem)
        {
            if (statusItem.Turns == 0)
            {
                ApplyStatusEffect(statusItem);
            }
            else
            {
                StatusType type = statusItem.Type;
                if (_currentStatuses.TryGetValue(type, out Status status))
                {
                    status.Turns += statusItem.Turns;
                }
                else
                {
                    Status newStatus = new Status(statusItem);
                    _currentStatuses[type] = newStatus;
                    _statusBar.AddStatusIcon(newStatus);
                }
            }
        }

        private void ApplyStatusEffect(StatusItem item)
        {
            StatusType type = item.Type;
            if (StatusType.Protection.Equals(type))
            {
                _character.Shield += item.Value;
            }
            else if (StatusType.Health.Equals(type))
            {
                _character.Health += item.Value;
            }
            else if (StatusType.Damage.Equals(type))
            {
                _character.TakeDamage(item.Value);
            }
            else if (StatusType.ThroughShieldDamage.Equals(type))
            {
                _character.TakeDamage(item.Value, true);
            }
        }

        public void Reset()
        {
            _statusBar.RemoveAllIcons();
            _currentStatuses.Clear();
        }

        public void Update()
        {
            List<StatusType> toRemove = new List<StatusType>();
            foreach (Status status in _currentStatuses.Values)
            {
                StatusItem item = status.Item;
                ApplyStatusEffect(item);
                status.Turns--;

                if (status.Turns == 0)
                {
                    toRemove.Add(item.Type);
                }
            }
            
            foreach (StatusType statusType in toRemove)
            {
                _currentStatuses.Remove(statusType);
            }
        }
    }
}