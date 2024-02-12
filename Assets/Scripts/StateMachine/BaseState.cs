namespace StateMachine
{
    public abstract class BaseState
    {
        public abstract void OnEnter();
        public abstract void OnTick();
        public abstract void OnExit();
    }
}
