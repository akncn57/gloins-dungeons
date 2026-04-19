using UnityEngine;

namespace Character.States
{
    public class DeathState : CharacterStateBase
    {
        public DeathState(CharacterController context, CharacterStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Animator.SetTrigger(CharacterAnimatorHashes.Death);
        }
    }
}