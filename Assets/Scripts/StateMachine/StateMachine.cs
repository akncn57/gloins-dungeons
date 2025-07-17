using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State CurrentState;

    public void ChangeState(State newState)
    {
        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = newState;

        if (CurrentState != null)
            CurrentState.Enter();
    }

    protected virtual void Update()
    {
        CurrentState?.Update();
    }

    protected virtual void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
}