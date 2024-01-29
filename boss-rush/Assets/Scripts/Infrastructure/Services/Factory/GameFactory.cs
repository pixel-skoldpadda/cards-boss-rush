using GameObjects.Character.Enemy;
using Infrastructure.Services.State;
using Infrastructure.States;
using Items.Boss;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IGameStateService _gameStateService;

        [Inject]
        public GameFactory(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
        
        //: TODO Костыль с передачей зависимости stateMachine починить
        public void CreateBossEnemy(BossEnemyItem item, IGameStateMachine stateMachine)
        {
            BossEnemy enemy = Object.Instantiate(item.Prefab, item.SpawnPoint, Quaternion.identity, null).GetComponent<BossEnemy>();
            enemy.Construct(item, _gameStateService, stateMachine);
            
            _gameStateService.State.Characters.Add(enemy);
        }
    }
}