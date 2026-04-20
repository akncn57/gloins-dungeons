using UnityEngine;

namespace Enemies.UndeadMage.States
{
    public class HeavyAttackState : UndeadMageBaseState
    {
        public HeavyAttackState(UndeadMageController context, UndeadMageStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetTrigger(UndeadMageAnimatorHashes.HeavyAttack);
            Context.Rb.linearVelocity = Vector2.zero;
        }

        public override void OnHeavyAttackAnimationHitCommand()
        {
            Context.HeavyAttackVFX.SetActive(true);
        }

        public override void OnHeavyAttackAnimationEndCommand()
        {
            Context.LastHeavyAttackTime = Time.time;
            StateMachine.ChangeState(UndeadMageStateMachine.ChaseState);
            Context.HeavyAttackVFX.SetActive(false);
        }
    }
}