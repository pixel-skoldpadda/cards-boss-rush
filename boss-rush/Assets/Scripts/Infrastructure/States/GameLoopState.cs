using Infrastructure.States.Interfaces;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        public GameLoopState(GameStateMachine gameStateMachine)
        {
            
        }

        public void Exit()
        {
            Debug.Log($"{GetType()} exited.");
        }

        public void Enter()
        {
            Debug.Log($"{GetType()} entered.");
        }
    }
}