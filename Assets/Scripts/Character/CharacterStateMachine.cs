using Character.States;
using StateMachine;

namespace Character
{
    public class PlayerStateMachine : BaseStateMachine<CharacterController>
    {
        public IdleState IdleState { get; }
        public WalkState WalkState { get; }
        public DashState DashState { get; }
        public LightAttackState LightAttackState { get; }
        public HeavyAttackState HeavyAttackState { get; }
        public HurtState HurtState { get; }
        public DeathState DeathState { get; }
        
        public PlayerStateMachine(CharacterController context)
        {
            IdleState = new IdleState(context, this);
            WalkState = new WalkState(context, this);
            DashState = new DashState(context, this);
            LightAttackState = new LightAttackState(context, this);
            HeavyAttackState = new HeavyAttackState(context, this);
            HurtState = new HurtState(context, this);
            DeathState = new DeathState(context, this);
            
            Initialize(IdleState);
        }
    }
}