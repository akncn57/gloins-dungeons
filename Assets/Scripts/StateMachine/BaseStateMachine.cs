namespace StateMachine
{
    public abstract class StateMachine<T>
    {
        public State<T> CurrentState { get; protected set; }
    
        public void Initialize(State<T> startingState)
        {
            CurrentState = startingState;
            CurrentState?.Enter();
        }

        public void ChangeState(State<T> newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public virtual void Update()
        {
            CurrentState?.Update();
        }

        public virtual void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
    }
}