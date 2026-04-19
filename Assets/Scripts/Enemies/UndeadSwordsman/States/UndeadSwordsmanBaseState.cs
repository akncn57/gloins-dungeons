using StateMachine;

namespace Enemies.UndeadSwordsman.States
{
    public class UndeadSwordsmanBaseState : State<EnemyBase>
    {
        protected readonly UndeadSwordsmanStateMachine UndeadSwordsmanStateMachine;
        protected new readonly UndeadSwordsmanController Context;
        
        public UndeadSwordsmanBaseState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine)
        {
            UndeadSwordsmanStateMachine = stateMachine;
            Context = context;
        }
        
        public virtual void OnLightAttackAnimationEndCommand() { }
        public virtual void OnHeavyAttackAnimationEndCommand() { }
        public virtual void OnHurtAnimationEndCommand() { }
    }
}