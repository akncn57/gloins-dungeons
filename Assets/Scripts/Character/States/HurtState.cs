using UnityEngine;

namespace Character.States
{
    public class HurtState : CharacterStateBase
    {
        public HurtState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Animator.SetTrigger(CharacterAnimatorHashes.Hurt);
        }

        public override void OnHurtAnimationEndCommand()
        {
            if (Context.Rb.linearVelocity.magnitude > 0.1f)
            {
                CharacterStateMachine.ChangeState(CharacterStateMachine.WalkState);
            }
            else
            {
                CharacterStateMachine.ChangeState(CharacterStateMachine.IdleState);
            }
        }
    }
}