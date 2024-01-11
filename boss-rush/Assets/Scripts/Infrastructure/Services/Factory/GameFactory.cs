using GameObjects.Player;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Items;
using Items;
using Items.Boss;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetsProvider _assetsProvider;
        private readonly IItemsService _items;

        [Inject]
        public GameFactory(DiContainer diContainer, IAssetsProvider assetsProvider, IItemsService items)
        {
            _diContainer = diContainer;
            _assetsProvider = assetsProvider;
            _items = items;
        }

        public void CreatePlayer()
        {
            PlayerItem item = _items.PlayerItem;
            Player player = _diContainer.InstantiatePrefabForComponent<Player>(item.Prefab, item.SpawnPoint, Quaternion.identity, null);
            _diContainer.Bind<Player>().FromInstance(player).AsSingle();
        }

        public void CreateBossEnemy(BossType type)
        {
            BossEnemyItem item = _items.GetBossEnemyItem(type);
            _diContainer.InstantiatePrefab(item.Prefab, item.SpawnPoint, Quaternion.identity, null);
        }
    }
}