using Infrastructure.Services.Assets;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Input;
using Infrastructure.Services.Items;
using Infrastructure.Services.Loader;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.State;
using Infrastructure.Services.WindowsManager;
using Infrastructure.States;
using Ui.Curtain;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain loadingCurtainPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IAssetsProvider>().To<AssetsProvider>().AsSingle();
            Container.Bind<IGameStateService>().To<GameStateService>().AsSingle();
            Container.Bind<IAudioService>().To<AudioService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IItemsService>().To<ItemsService>().AsSingle();
            Container.Bind<IUiFactory>().To<UiFactory>().AsSingle();
            Container.Bind<IWindowsManager>().To<WindowsManager>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();

            BindLoadingCurtain();
        }

        private void BindLoadingCurtain()
        { 
            LoadingCurtain loadingCurtain = 
                Container.InstantiatePrefabForComponent<LoadingCurtain>(loadingCurtainPrefab, Vector3.zero, Quaternion.identity, null);

            Container.Bind<LoadingCurtain>().FromInstance(loadingCurtain).AsSingle();
        }
    }
}