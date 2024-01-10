using Infrastructure.Services.Loader;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private const int MaxFrameRate = 60;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, IGameStateMachine gameStateMachine)
        {
            sceneLoader.Init(this);
            gameStateMachine.Enter<BootstrapState>();
        }
        
        private void Awake()
        {
            Application.targetFrameRate = MaxFrameRate;
            
            DontDestroyOnLoad(this);
        }
    }
}