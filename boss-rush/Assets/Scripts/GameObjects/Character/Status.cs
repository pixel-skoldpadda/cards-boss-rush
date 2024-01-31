using System;
using Items.Card;

namespace GameObjects.Character
{
    public class Status
    {
        private readonly StatusItem _item;
        private int _turns;

        public Action<int> OnTurnsUpdated { get; set; }

        public Status(StatusItem item)
        {
            _item = item;
            _turns = item.Turns;
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
        
        public StatusItem Item => _item;
    }
}