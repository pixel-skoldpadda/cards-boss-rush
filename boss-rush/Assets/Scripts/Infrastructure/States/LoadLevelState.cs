using Infrastructure.Services.Loader;
using Infrastructure.States.Interfaces;
using Ui.Curtain;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
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
    
        }
    }
}