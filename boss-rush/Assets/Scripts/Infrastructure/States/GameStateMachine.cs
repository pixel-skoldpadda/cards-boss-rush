﻿using System;
using System.Collections.Generic;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Items;
using Infrastructure.Services.Loader;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.State;
using Infrastructure.States.Interfaces;
using Ui.Curtain;
using Zenject;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public GameStateMachine(ISceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameStateService gameStateService, ISaveLoadService saveLoadService,
            IAudioService audio, IItemsService items, IGameFactory gameFactory)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, items),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, gameFactory),
                [typeof(LoadSceneState)] = new LoadSceneState(sceneLoader, loadingCurtain),
                [typeof(LoadProgressState)] = new LoadProgressState(this, gameStateService, saveLoadService, audio),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }
    
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}