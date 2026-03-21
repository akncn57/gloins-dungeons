namespace StateMachine
{
    public abstract class State<T>
    {
        protected BaseStateMachine<T> BaseStateMachine;
        protected T Context;

        protected State(T context, BaseStateMachine<T> baseStateMachine)
        {
            Context = context;
            BaseStateMachine = baseStateMachine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}