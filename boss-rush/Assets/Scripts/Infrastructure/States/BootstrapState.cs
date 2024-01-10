using Infrastructure.Services.Items;
using Infrastructure.States.Interfaces;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IItemsService _items;

        public BootstrapState(IGameStateMachine stateMachine, IItemsService items)
        {
            _stateMachine = stateMachine;
            _items = items;

            RegisterServices();
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");

            _items.LoadAllItems();
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }
        
        private void RegisterServices()
        {

        }
    }
}