namespace Character.States
{
    public class WalkState : CharacterStateBase
    {
        public WalkState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetBool(CharacterAnimatorHashes.IsMoving, true);
        }

        public override void Update()
        {
            if (Context.MovementInput.magnitude < 0.1f)
            {
                CharacterStateMachine.ChangeState(CharacterStateMachine.IdleState); 
            }
            
            if (Context.MovementInput.x != 0)
            {
                Context.SpriteRenderer.flipX = Context.MovementInput.x < 0;
            }
            
            Context.CameraShake.TriggerShake(0.3f, 0.3f);
        }

        public override void FixedUpdate()
        {
            Context.Rb.linearVelocity = Context.MovementInput.normalized * Context.CharacterStats.MoveSpeed;
        }

        public override void Exit()
        {
            Context.Animator.SetBool(CharacterAnimatorHashes.IsMoving, false);
        }

        public override void OnDashCommand()
        {
            if (Context.CanDash())
            {
                CharacterStateMachine.ChangeState(CharacterStateMachine.DashState);
            }
        }
        
        public override void OnLightAttackCommand()
        {
            CharacterStateMachine.ChangeState(CharacterStateMachine.LightAttackState);
        }

        public override void OnHeavyAttackCommand()
        {
            if (Context.CanHeavyAttack())
            {
                CharacterStateMachine.ChangeState(CharacterStateMachine.HeavyAttackState);
            }
        }

        public override void OnHurtCommand()
        {
            CharacterStateMachine.ChangeState(CharacterStateMachine.HurtState);
        }
    }
}