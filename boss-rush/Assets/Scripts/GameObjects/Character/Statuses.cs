using System.Collections.Generic;
using Data;
using Items.Card;
using Ui;

namespace GameObjects.Character
{
    public class Statuses
    {
        private readonly Character _character;
        private readonly StatusBar _statusBar;
        private readonly GameState _gameState;
        
        private readonly List<Status> _activeStatuses = new();
        
        public Statuses(Character character, StatusBar statusBar, GameState gameState)
        {
            _character = character;
            _statusBar = statusBar;
            _gameState = gameState;
        }

        public void AddStatus(Status status)
        {
            if (status.IsInstantaneous())
            {
                ApplyStatusEffect(status);
            }
            else
            {
                Status currentStatus = GetStatusById(status.ID);
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

        public void RemoveStatus(Status status)
        {
            _statusBar.RemoveStatusIconByType(status.Item.Type);
            _activeStatuses.Remove(status);
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

        private Status GetStatusById(string id)
        {
            foreach (Status status in _activeStatuses)
            {
                if (id.Equals(status.ID))
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

        public List<Status> ActiveStatuses => _activeStatuses;

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
                ApplyDamageEffect(status.Value, true);
            }
            else if (StatusType.Poison.Equals(type))
            {
                _character.TakeDamage(status.Value, true);
            }
        }

        private void ApplyDamageEffect(int value, bool throughShield = false)
        {
            TryReturnDamage(value);
            
            int damage = value;
            foreach (Status status in _activeStatuses)
            {
                if (StatusType.Vulnerability.Equals(status.Item.Type))
                {
                    damage += damage * status.Value / 100;
                }
            }
       
            _character.TakeDamage(damage, throughShield);
        }

        private void TryReturnDamage(int damage)
        {
            Character activeCharacter = _gameState.ActiveCharacter;
            if (activeCharacter.Equals(_character))
            {
                return;
            }
            
            int returnDamage = 0;
            foreach (Status status in _activeStatuses)
            {
                if (StatusType.Thorns.Equals(status.Item.Type))
                {
                    returnDamage += damage * status.Value / 100;
                }
            }

            if (returnDamage > 0)
            {
                activeCharacter.TakeDamage(returnDamage);
            }
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