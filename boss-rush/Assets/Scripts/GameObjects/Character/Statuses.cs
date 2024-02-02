using System.Collections.Generic;
using Items.Card;
using Ui;

namespace GameObjects.Character
{
    public class Statuses
    {
        private readonly Character _character;
        private readonly StatusBar _statusBar;
        private readonly List<Status> _activeStatuses = new();
        
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
                Status status = GetStatus(statusItem.Type);
                if (status != null)
                {
                    status.Turns += statusItem.Turns;
                }
                else
                {
                    Status newStatus = new Status(statusItem);
                    _activeStatuses.Add(newStatus);
                    _statusBar.AddStatusIcon(newStatus);
                }
            }
        }

        public void Reset()
        {
            _statusBar.RemoveAllIcons();
            _activeStatuses.Clear();
        }

        public void Update()
        {
            List<Status> toRemove = new List<Status>();
            foreach (Status status in _activeStatuses)
            {
                StatusItem item = status.Item;
                ApplyStatusEffect(item);
                status.Turns--;

                if (status.Turns == 0)
                {
                    toRemove.Add(status);
                }
            }
            
            foreach (Status status in toRemove)
            {
                _activeStatuses.Remove(status);
            }
        }

        public Status GetStatus(StatusType type)
        {
            foreach (Status status in _activeStatuses)
            {
                if (type.Equals(status.Item.Type))
                {
                    return status;
                }
            }
            return null;
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
                ApplyHealthEffect(item);
            }
            else if (StatusType.Damage.Equals(type))
            {
                ApplyDamageEffect(item);
            }
            else if (StatusType.ThroughShieldDamage.Equals(type))
            {
                _character.TakeDamage(item.Value, true);
            }
            else if (StatusType.Poison.Equals(type))
            {
                _character.TakeDamage(item.Value, true);
            }
        }

        private void ApplyDamageEffect(StatusItem item)
        {
            int damage = item.Value;
            
            Status status = GetStatus(StatusType.Vulnerability);
            if (status != null)
            {
                damage += damage * status.Item.Value / 100;
            }
            _character.TakeDamage(damage);
        }

        private void ApplyHealthEffect(StatusItem item)
        {
            int health = item.Value;
            foreach (Status status in _activeStatuses)
            {
                StatusItem statusItem = status.Item;
                StatusType statusType = statusItem.Type;

                if (StatusType.IncreasedHealth.Equals(statusType))
                {
                    health += health * statusItem.Value / 100;
                }
                else if (StatusType.DecreasedHealth.Equals(statusType))
                {
                    health -= health * statusItem.Value / 100;
                }
            }
            _character.Health += health;
        }
    }
}