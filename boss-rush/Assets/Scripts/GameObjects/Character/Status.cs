using Items.Card;

namespace GameObjects.Character
{
    public class Status
    {
        private readonly StatusItem _item;
        private int _turns;
        
        public Status(StatusItem item)
        {
            _item = item;
            _turns = item.Turns;
        }

        public StatusItem Item => _item;

        public void AddTurns(int turns)
        {
            _turns += turns;
        }
        
        public int Turns => _turns;
    }
}