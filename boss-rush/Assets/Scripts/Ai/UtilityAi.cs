using System.Collections.Generic;
using GameObjects.Character;
using GameObjects.Character.Enemy;
using Items.Boss.AI;
using Items.Card;
using UnityEngine;

namespace Ai
{
    public class UtilityAi
    {
        private readonly UtilityAiItem _utilityAiItem;
        private readonly BossEnemy _boss;
        private readonly Character _player;
        
        private readonly List<Action> _actions = new();
        
        public UtilityAi(UtilityAiItem utilityAiItem, BossEnemy boss, Character player)
        {
            _utilityAiItem = utilityAiItem;
            _boss = boss;
            _player = player;

            InitActions();
        }

        public Action ChooseBestAction()
        {
            if (_utilityAiItem.ActionsLogging)
            {
                Debug.Log("<color=red>Choose best action</color>");
            }
            
            foreach (Action action in _actions)
            {
                UtilityAiAction actionItem = action.ActionItem;
                StatusSubtype subtype = actionItem.StatusSubtype;
                
                int maxHealth = StatusSubtype.Negative.Equals(subtype) ? _player.Item.MaxHealth : _boss.Item.MaxHealth;
                int currentHealth = StatusSubtype.Negative.Equals(subtype) ? _player.Health : _boss.CalculateHealth();
                action.CalculateGrade(currentHealth, maxHealth);
            }

            Action bestAction = _actions[0];
            foreach (Action action in _actions)
            {
                if (bestAction.Score < action.Score)
                {
                    bestAction = action;
                }
            }

            if (_utilityAiItem.ActionsLogging)
            {
                UtilityAiAction item = bestAction.ActionItem;
                Debug.Log($"<color=green>Best action type: {item.StatusType}, subtype {item.StatusSubtype} </color>.");
            }
            
            return bestAction;
        }
        
        private void InitActions()
        {
            foreach (UtilityAiAction aiAction in _utilityAiItem.AIActions)
            {
                _actions.Add(new Action(aiAction, _utilityAiItem.MaxScore, _boss.CardsDeck, _utilityAiItem.ActionsLogging));
            }
        }
    }
}