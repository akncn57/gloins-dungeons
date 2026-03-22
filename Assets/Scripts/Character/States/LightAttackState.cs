using UnityEngine;

namespace Character.States
{
    public class LightAttackState : CharacterStateBase
    {
        public LightAttackState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Animator.SetTrigger(CharacterAnimatorHashes.LightAttack);
        }

        public override void OnLightAttackAnimationEndCommand()
        {
            if (Context.MovementInput.magnitude > 0.1)
            {
                StateMachine.ChangeState(CharacterStateMachine.WalkState);
            }
            else
            {
                StateMachine.ChangeState(CharacterStateMachine.IdleState);
            }
        }
    }
}