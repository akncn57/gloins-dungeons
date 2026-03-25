using Character.States;
using StateMachine;

namespace Character
{
    public class CharacterStateMachine : BaseStateMachine<CharacterController>
    {
        public IdleState IdleState { get; }
        public WalkState WalkState { get; }
        public DashState DashState { get; }
        public LightAttackState LightAttackState { get; }
        public HeavyAttackState HeavyAttackState { get; }
        public HurtState HurtState { get; }
        public DeathState DeathState { get; }
        
        public CharacterStateMachine(CharacterController context)
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
        
        public void OnLightAttackPressed()
        {
            (CurrentState as CharacterStateBase)?.OnLightAttackCommand();
        }
        
        public void OnLightAttackAnimationEnd()
        {
            (CurrentState as CharacterStateBase)?.OnLightAttackAnimationEndCommand();
        }

        public void OnHeavyAttackPressed()
        {
            (CurrentState as CharacterStateBase)?.OnHeavyAttackCommand();
        }
        
        public void OnHeavyAttackAnimationEnd()
        {
            (CurrentState as CharacterStateBase)?.OnHeavyAttackAnimationEndCommand();
        }

        public void OnDashPressed()
        {
            (CurrentState as CharacterStateBase)?.OnDashCommand();
        }

        public void OnHurt()
        {
            (CurrentState as CharacterStateBase)?.OnHurtCommand();
        }

        public void OnHurtAnimationEnd()
        {
            (CurrentState as CharacterStateBase)?.OnHurtAnimationEndCommand();
        }
    }
}