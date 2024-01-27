using System.Collections.Generic;
using GameObjects.Character.Player;
using Infrastructure.Services.Items;
using Infrastructure.Services.State;
using Items;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        private IItemsService _items;
        private IGameStateService _gameStateService;

        [Inject]
        public void Construct(IItemsService items, IGameStateService gameStateService)
        {
            _items = items;
            _gameStateService = gameStateService;
        }
        
        public override void InstallBindings()
        {
            PlayerItem item = _items.PlayerItem;
            Player player = Container.InstantiatePrefabForComponent<Player>(item.Prefab, item.SpawnPoint, Quaternion.identity, null,
                new List<PlayerItem> { item });
            
            _gameStateService.State.Characters.Add(player);
        }
    }
}