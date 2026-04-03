using UnityEngine;

namespace Enemies.UndeadSwordsman.States
{
    public class IdleState : UndeadSwordsmanBaseState
    {
        public IdleState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Rb.linearVelocity = Vector2.zero;
        }
        public override void Update()
        {
            if (Context.PlayerTarget != null)
            {
                float dist = Vector2.Distance(Context.transform.position, Context.PlayerTarget.position);
                if (dist <= Context.EnemyStats.ChaseRange)
                {
                    StateMachine.ChangeState(UndeadSwordsmanStateMachine.ChaseState);
                }
            }
        }
    }
}