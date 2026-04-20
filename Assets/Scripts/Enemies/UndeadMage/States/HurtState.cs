using UnityEngine;

namespace Enemies.UndeadMage.States
{
    public class HurtState : UndeadMageBaseState
    {
        public HurtState(UndeadMageController context, UndeadMageStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetTrigger(UndeadMageAnimatorHashes.Hurt);
        }

        public override void OnHurtAnimationEndCommand()
        {
            if (Context.PlayerTarget != null)
            {
                StateMachine.ChangeState(UndeadMageStateMachine.ChaseState);
            }
            else
            {
                StateMachine.ChangeState(UndeadMageStateMachine.IdleState);
            }
        }
    }
}