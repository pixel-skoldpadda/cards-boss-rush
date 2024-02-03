using System;
using Items.Card;

namespace GameObjects.Character
{
    public class Status
    {
        private readonly StatusItem _item;
        private int _turns;
        private int _value;

        public Action<int> OnTurnsUpdated { get; set; }

        public Status(StatusItem item)
        {
            _item = item;
            _turns = item.Turns;
            _value = item.Value;
        }

        public int Turns
        {
            get => _turns;
            set
            {
                _turns = value;
                OnTurnsUpdated?.Invoke(_turns);
            }
        }

        public int Value
        {
            get => _value;
            set => _value = value;
        }

        public bool IsInstantaneous()
        {
            return _turns == 0;
        }
        
        public StatusItem Item => _item;
    }
}