using System.Collections.Generic;
using GameObjects.Character.Enemy;
using GameObjects.Character.Player;
using Infrastructure.Services.Items;
using Infrastructure.Services.State;
using Items;
using Items.Boss;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IItemsService _items;
        private readonly IGameStateService _gameStateService;

        [Inject]
        public GameFactory(DiContainer diContainer, IItemsService items, IGameStateService gameStateService)
        {
            _diContainer = diContainer;
            _gameStateService = gameStateService;
            _items = items;
        }

        public void CreatePlayer()
        {
            PlayerItem item = _items.PlayerItem;
            Player player = _diContainer.InstantiatePrefabForComponent<Player>(item.Prefab, item.SpawnPoint, Quaternion.identity, null,
                new List<PlayerItem> { item });
            
            _gameStateService.State.Characters.Add(player);
            _diContainer.Bind<Player>().FromInstance(player).AsSingle();
        }

        public void CreateBossEnemy(BossType type)
        {
            BossEnemyItem item = _items.GetBossEnemyItem(type);
            BossEnemy bossEnemy = _diContainer.InstantiatePrefabForComponent<BossEnemy>(item.Prefab, item.SpawnPoint, Quaternion.identity, null, 
                new List<BossEnemyItem> { item });
            
            _gameStateService.State.Characters.Add(bossEnemy);
        }
    }
}