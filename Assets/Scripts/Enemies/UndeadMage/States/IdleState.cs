using UnityEngine;

namespace Enemies.UndeadMage.States
{
    public class IdleState : UndeadMageBaseState
    {
        public IdleState(UndeadMageController context, UndeadMageStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
            Context.Animator.SetBool(UndeadMageAnimatorHashes.IsWalking, false);
        }

        public override void Update()
        {
            if (Context.PlayerTarget != null)
            {
                var distToPlayer = Vector2.Distance(Context.transform.position, Context.PlayerTarget.position);
                
                if (distToPlayer <= Context.EnemyStats.ChaseRange)
                {
                    StateMachine.ChangeState(UndeadMageStateMachine.ChaseState);
                }
            }
        }
    }
}