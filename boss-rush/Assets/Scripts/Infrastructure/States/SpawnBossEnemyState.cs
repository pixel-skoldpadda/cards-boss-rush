using System.Collections.Generic;
using Configs;
using Data;
using Infrastructure.Services.Factory;
using Infrastructure.Services.State;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States.Interfaces;
using Items.Boss;
using ModestTree;
using Ui.Windows;
using UnityEngine;

namespace Infrastructure.States
{
    public class SpawnBossEnemyState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameStateService _gameStateService;
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowsManager _windowsManager;

        public SpawnBossEnemyState(IGameStateMachine stateMachine, IGameFactory gameFactory, IGameStateService gameStateService, IWindowsManager windowsManager)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _gameStateService = gameStateService;
            _windowsManager = windowsManager;
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            GameState gameState = _gameStateService.State;
            Queue<BossEnemyItem> queue = gameState.BossesQueue;
            if (queue.IsEmpty())
            {
                gameState.HUD.Hide();
                _windowsManager.OpenWindow(WindowType.CongratulationsWindow, true);
            }
            else
            {
                gameState.HUD.Show();
                _gameFactory.CreateBossEnemy(queue.Dequeue(), _stateMachine);
            }
            
            _stateMachine.Enter<StepTransitionState>();
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
    }
}