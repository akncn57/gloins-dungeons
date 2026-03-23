using StateMachine;

namespace Character.States
{
    public abstract class CharacterStateBase : State<CharacterController>
    {
        protected readonly CharacterStateMachine CharacterStateMachine;

        protected CharacterStateBase(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine)
        {
            CharacterStateMachine = stateMachine;
        }
        
        public virtual void OnLightAttackCommand() { }
        public virtual void OnLightAttackAnimationEndCommand() { }
        public virtual void OnHeavyAttackCommand() { }
        public virtual void OnHeavyAttackAnimationEndCommand() { }
        public virtual void OnDashCommand() { }
    }
}