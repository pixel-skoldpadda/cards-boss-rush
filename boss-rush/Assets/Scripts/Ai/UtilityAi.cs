using System.Collections.Generic;
using GameObjects.Character;
using Items.Boss.AI;
using Items.Card;

namespace Ai
{
    public class UtilityAi
    {
        private readonly UtilityAiItem _utilityAiItem;
        private readonly Character _boss;
        private readonly Character _player;
        
        private readonly List<Action> _actions = new();
        
        public UtilityAi(UtilityAiItem utilityAiItem, Character boss, Character player)
        {
            _utilityAiItem = utilityAiItem;
            _boss = boss;
            _player = player;

            InitActions();
        }

        public Action ChooseBestAction()
        {
            foreach (Action action in _actions)
            {
                CardType type = action.CardType;
                if (CardType.Attack.Equals(type))
                {
                    action.CalculateGrade(_player.Health, _player.Item.MaxHealth);
                }
                else if (CardType.Protection.Equals(type))
                {
                    action.CalculateGrade(_boss.Health, _boss.Item.MaxHealth);
                }
            }

            Action bestAction = _actions[0];
            foreach (Action action in _actions)
            {
                if (bestAction.Score < action.Score)
                {
                    bestAction = action;
                }
            }

            return bestAction;
        }
        
        private void InitActions()
        {
            foreach (UtilityAiAction aiAction in _utilityAiItem.AIActions)
            {
                _actions.Add(new Action(aiAction, _utilityAiItem.MaxScore, _boss.CardsDeck));
            }
        }
    }
}