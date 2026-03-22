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
        }

        public override void FixedUpdate()
        {
            Context.Rb.linearVelocity = Context.MovementInput.normalized * Context.CharacterStats.MoveSpeed;
        }
    }
}