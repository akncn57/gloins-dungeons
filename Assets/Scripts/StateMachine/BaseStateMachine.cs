using UnityEngine;

namespace StateMachine
{
    public class BaseStateMachine : MonoBehaviour
    {
        private BaseState _currentState;

        private void Update()
        {
            _currentState?.OnTick();
        }
        
        public void SwitchState(BaseState newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState?.OnEnter();
        }
    }
}
