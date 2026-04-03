using Enemies.UndeadSwordsman.States;

namespace Enemies.UndeadSwordsman
{
    public class UndeadSwordsmanStateMachine : EnemyStateMachine
    {
        public IdleState IdleState { get; }
        public ChaseState ChaseState { get; }
        public LightAttackState LightAttackState { get; }
        public HeavyAttackState HeavyAttackState { get; }
        
        public UndeadSwordsmanStateMachine(UndeadSwordsmanController context)
        {
            IdleState = new IdleState(context, this);
            ChaseState = new ChaseState(context, this);
            LightAttackState = new LightAttackState(context, this);
            HeavyAttackState = new HeavyAttackState(context, this);
            HurtState = new HurtState(context, this);
            DeathState = new DeathState(context, this);
            
            Initialize(IdleState);
        }
    }
}