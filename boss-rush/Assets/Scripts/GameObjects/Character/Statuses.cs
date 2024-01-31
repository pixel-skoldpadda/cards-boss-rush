using System.Collections.Generic;
using Data;
using Items.Card;

namespace GameObjects.Character
{
    public class Statuses
    {
        private readonly Character _character;
        
        private Dictionary<StatusType, Status> _currentStatuses = new();
        
        public Statuses(Character character)
        {
            _character = character;
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
                    status.AddTurns(statusItem.Turns);
                }
                else
                {
                    _currentStatuses[type] = new Status(statusItem);
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
                _character.Health -= item.Value;
            }
        }
    }
}