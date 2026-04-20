using Enemies.UndeadMage.States;

namespace Enemies.UndeadMage
{
    public class UndeadMageStateMachine : EnemyStateMachine
    {
        public IdleState IdleState { get; }
        public ChaseState ChaseState { get; }
        public LightAttackState LightAttackState { get; }
        public HeavyAttackState HeavyAttackState { get; }

        public UndeadMageStateMachine(UndeadMageController context)
        {
            IdleState = new IdleState(context, this);
            ChaseState = new ChaseState(context, this);
            LightAttackState = new LightAttackState(context, this);
            HeavyAttackState = new HeavyAttackState(context, this);
            HurtState = new HurtState(context, this);
            DeathState = new DeathState(context, this);
            
            Initialize(IdleState);
        }

        public void OnLightAttackInitAnimationEnd()
        {
            (CurrentState as UndeadMageBaseState)?.OnLightAttackInitAnimationEndCommand();
        }

        public void OnLightAttackFinalAnimationEnd()
        {
            (CurrentState as UndeadMageBaseState)?.OnLightAttackFinalAnimationEndCommand();
        }

        public void OnHeavyAttackAnimationEnd()
        {
            (CurrentState as UndeadMageBaseState)?.OnHeavyAttackAnimationEndCommand();
        }

        public void OnHurtAnimationEnd()
        {
            (CurrentState as UndeadMageBaseState)?.OnHurtAnimationEndCommand();
        }
    }
}