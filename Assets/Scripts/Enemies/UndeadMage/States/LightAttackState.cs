using UnityEngine;

namespace Enemies.UndeadMage.States
{
    public class LightAttackState : UndeadMageBaseState
    {
        public LightAttackState(UndeadMageController context, UndeadMageStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetTrigger(UndeadMageAnimatorHashes.LightAttackInit);
            Context.Rb.linearVelocity = Vector2.zero;
        }

        public override void OnLightAttackInitAnimationEndCommand()
        {
            Context.Animator.SetTrigger(UndeadMageAnimatorHashes.LightAttackFinal);
        }

        public override void OnLightAttackFinalAnimationEndCommand()
        {
            Context.LastLightAttackTime = Time.time;
            StateMachine.ChangeState(UndeadMageStateMachine.IdleState);
        }
    }
}