using UnityEngine;

namespace Character.States
{
    public class IdleState : CharacterStateBase
    {
        public IdleState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Animator.SetBool(CharacterAnimatorHashes.IsMoving, false);
        }

        public override void Update()
        {
            if (Context.MovementInput.magnitude > 0.1f)
            {
                CharacterStateMachine.ChangeState(CharacterStateMachine.WalkState); 
            }
        }

        public override void OnDashCommand()
        {
            if (Context.CanDash())
            {
                CharacterStateMachine.ChangeState(CharacterStateMachine.DashState);
            }
        }
    }
}