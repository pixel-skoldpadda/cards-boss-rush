using GameObjects.Character;
using Items.Boss.AI;
using Items.Card;

namespace Ai
{
    public class Action
    {
        private readonly UtilityAiAction _actionItem;
        private readonly int _maxScore;
        private readonly CardsDeck _cardsDeck;

        private float _score;
        
        public Action(UtilityAiAction actionItem, int maxScore, CardsDeck cardsDeck)
        {
            _actionItem = actionItem;
            _maxScore = maxScore;
            _cardsDeck = cardsDeck;
        }

        public void CalculateGrade(int currentValue, int maxValue)
        {
            if (!_cardsDeck.HasAnyCardInHand(_actionItem.CardType))
            {
                _score = -1;
            }
            else
            {
                float function = (float) currentValue * _maxScore / maxValue;
                _score = _actionItem.UseActionCurve.Evaluate(function);
            }
        }

        public CardType CardType => _actionItem.CardType;
        
        public float Score
        {
            get => _score;
            set => _score = value;
        }
    }
}