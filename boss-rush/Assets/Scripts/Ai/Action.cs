using GameObjects.Character;
using Items.Boss.AI;
using UnityEngine;

namespace Ai
{
    public class Action
    {
        private readonly UtilityAiAction _actionItem;
        private readonly int _maxScore;
        private readonly CardsDeck _cardsDeck;
        private readonly bool _actionsLogging;

        private float _score;
        
        public Action(UtilityAiAction actionItem, int maxScore, CardsDeck cardsDeck, bool actionsLogging)
        {
            _actionItem = actionItem;
            _maxScore = maxScore;
            _cardsDeck = cardsDeck;
            _actionsLogging = actionsLogging;
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

            if (_actionsLogging)
            {
                Debug.Log($"<color=yellow>Action</color> type: {_actionItem.StatusType}, subtype {_actionItem.StatusSubtype}, score : <color=green>{_score}/{1}</color>.");
            }
        }
        
        public UtilityAiAction ActionItem => _actionItem;
        public float Score => _score;
    }
}