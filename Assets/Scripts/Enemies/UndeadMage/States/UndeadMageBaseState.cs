using StateMachine;

namespace Enemies.UndeadMage.States
{
    public class UndeadMageBaseState : State<EnemyBase>
    {
        protected readonly UndeadMageStateMachine UndeadMageStateMachine;
        protected new readonly UndeadMageController Context;
        
        public UndeadMageBaseState(UndeadMageController context, UndeadMageStateMachine stateMachine) : base(context, stateMachine)
        {
            UndeadMageStateMachine = stateMachine;
            Context = context;
        }
    }
}