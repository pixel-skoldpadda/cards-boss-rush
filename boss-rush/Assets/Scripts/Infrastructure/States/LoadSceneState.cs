using Infrastructure.Services.Loader;
using Infrastructure.States.Interfaces;
using Ui.Curtain;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadSceneState(ISceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string payload)
        {
            Debug.Log($"{GetType()} entered.");
            
            _loadingCurtain.Show();
            _sceneLoader.Load(payload, OnSceneLoaded);
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
        
        private void OnSceneLoaded()
        {
            _loadingCurtain.Hide();
        }
    }
}