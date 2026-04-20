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

        public virtual void OnLightAttackInitAnimationEndCommand() {}
        public virtual void OnLightAttackFinalAnimationEndCommand() {}
        public virtual void OnHeavyAttackAnimationEndCommand() {}
        public virtual void OnHurtAnimationEndCommand() {}
    }
}