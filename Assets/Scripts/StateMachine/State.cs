namespace StateMachine
{
    public abstract class State<T>
    {
        protected BaseStateMachine<T> StateMachine;
        protected T Context;

        protected State(T context, BaseStateMachine<T> stateMachine)
        {
            Context = context;
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}