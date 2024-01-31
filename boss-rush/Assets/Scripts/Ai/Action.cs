using GameObjects.Character;
using Items.Boss.AI;

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
            if (!_cardsDeck.HasAnyCardInHandWithStatus(_actionItem.StatusType, _actionItem.StatusSubtype))
            {
                _score = -1;
            }
            else
            {
                float function = (float) currentValue * _maxScore / maxValue;
                _score = _actionItem.UseActionCurve.Evaluate(function);
            }
        }
        
        public UtilityAiAction ActionItem => _actionItem;
        public float Score => _score;
    }
}