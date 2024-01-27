using System.Collections.Generic;
using GameObjects.Character.Enemy;
using Infrastructure.Services.Items;
using Infrastructure.Services.State;
using Items.Boss;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IItemsService _items;
        private readonly IGameStateService _gameStateService;

        [Inject]
        public GameFactory(IInstantiator instantiator, IItemsService items, IGameStateService gameStateService)
        {
            _instantiator = instantiator;
            _gameStateService = gameStateService;
            _items = items;
        }

        public void CreateBossEnemy(BossType type)
        {
            BossEnemyItem item = _items.GetBossEnemyItem(type);
            BossEnemy bossEnemy = _instantiator.InstantiatePrefabForComponent<BossEnemy>(item.Prefab, item.SpawnPoint, Quaternion.identity, null, 
                new List<BossEnemyItem> { item });
            
            _gameStateService.State.Characters.Add(bossEnemy);
        }
    }
}