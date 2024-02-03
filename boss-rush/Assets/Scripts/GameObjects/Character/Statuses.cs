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

        public void AddStatus(Status status)
        {
            if (status.IsInstantaneous())
            {
                ApplyStatusEffect(status);
            }
            else
            {
                Status currentStatus = GetStatus(status.Item.Type);
                if (currentStatus != null)
                {
                    currentStatus.Turns += status.Turns;
                }
                else
                {
                    _activeStatuses.Add(status);
                    _statusBar.AddStatusIcon(status);
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
                ApplyStatusEffect(new Status(status.Item));
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

        // TODO: Временное решение, по хорошему нужно выделить обратно типы карт с главным эффектом типо урона, а статусы применять отдельно.
        public int CalculateDamageEffect(int value)
        {
            int damage = value;
            foreach (Status status in _activeStatuses)
            {
                StatusItem statusItem = status.Item;
                StatusType statusType = statusItem.Type;

                if (StatusType.IncreasedDamage.Equals(statusType))
                {
                    damage += damage * statusItem.Value / 100;
                }
                else if (StatusType.DecreasedDamage.Equals(statusType))
                {
                    damage -= damage * statusItem.Value / 100;
                }
            }
            return damage;
        }
        
        private void ApplyStatusEffect(Status status)
        {
            StatusType type = status.Item.Type;
            if (StatusType.Protection.Equals(type))
            {
                _character.Shield += status.Value;
            }
            else if (StatusType.Health.Equals(type))
            {
                ApplyHealthEffect(status.Value);
            }
            else if (StatusType.Damage.Equals(type))
            {
                ApplyDamageEffect(status.Value);
            }
            else if (StatusType.ThroughShieldDamage.Equals(type))
            {
                _character.TakeDamage(status.Value, true);
            }
            else if (StatusType.Poison.Equals(type))
            {
                _character.TakeDamage(status.Value, true);
            }
        }

        private void ApplyDamageEffect(int value)
        {
            int damage = value;
            Status status = GetStatus(StatusType.Vulnerability);
            if (status != null)
            {
                damage += damage * status.Item.Value / 100;
            }
            _character.TakeDamage(damage);
        }

        private void ApplyHealthEffect(int value)
        {
            int health = value;
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