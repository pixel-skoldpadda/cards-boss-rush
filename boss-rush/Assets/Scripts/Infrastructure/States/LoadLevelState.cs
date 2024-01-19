using Infrastructure.Services.Factory;
using Infrastructure.Services.Loader;
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
        private readonly IGameFactory _gameFactory;
        private readonly IUiFactory _uiFactory;

        public LoadLevelState(
            GameStateMachine stateMachine, ISceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IUiFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
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
            InitGameWorld();
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.CreatePlayer();
            _gameFactory.CreateBossEnemy(BossType.Kinght);
            _uiFactory.CreateHud();
        }
    }
}