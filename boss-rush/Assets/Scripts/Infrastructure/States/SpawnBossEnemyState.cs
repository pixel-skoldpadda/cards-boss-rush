using Configs;
using Infrastructure.Services.Factory;
using Infrastructure.Services.State;
using Infrastructure.States.Interfaces;
using Items.Boss;
using UnityEngine;

namespace Infrastructure.States
{
    public class SpawnBossEnemyState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameStateService _gameStateService;
        private readonly IGameStateMachine _stateMachine;

        public SpawnBossEnemyState(IGameStateMachine stateMachine, IGameFactory gameFactory, IGameStateService gameStateService)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _gameStateService = gameStateService;
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            BossEnemyItem enemyItem = _gameStateService.State.BossesQueue.Dequeue();
            if (enemyItem == null)
            {
                //: TODO Show thanks for playing dialog
                _stateMachine.Enter<LoadSceneState, string>(SceneConfig.MenuScene);
            }
            else
            {
                _gameStateService.State.HUD.Show();
                _gameFactory.CreateBossEnemy(enemyItem, _stateMachine);
            }
            
            _stateMachine.Enter<StepTransitionState>();
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}