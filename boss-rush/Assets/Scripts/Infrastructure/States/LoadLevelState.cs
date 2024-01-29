using System.Collections.Generic;
using Infrastructure.Services.Items;
using Infrastructure.Services.Loader;
using Infrastructure.Services.State;
using Infrastructure.States.Interfaces;
using Items.Boss;
using Ui.Curtain;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IItemsService _items;
        private readonly IGameStateService _gameStateService;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, LoadingCurtain loadingCurtain, IItemsService items, 
            IGameStateService gameStateService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _items = items;
            _gameStateService = gameStateService;
        }

        public void Enter(string sceneName)
        {
            Debug.Log($"{GetType()} entered. Scene name: {sceneName}");
            
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
            
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            Queue<BossEnemyItem> queue = _gameStateService.State.BossesQueue;
            foreach (BossEnemyItem enemyItem in _items.BossEnemyItems)
            {
                queue.Enqueue(enemyItem);
            }

            _stateMachine.Enter<SpawnBossEnemyState>();
        }
    }
}