using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonPatrolState : EnemyBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        
        public SkeletonPatrolState(EnemyBaseStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void OnEnter()
        {
            
        }

        public override void OnTick()
        {
            DrawChaseOverlayAndCheck();
        }

        public override void OnExit()
        {
            
        }

        private void DrawChaseOverlayAndCheck()
        {
            var result = Physics2D.OverlapCircle(EnemyStateMachine.ChaseCollider.transform.position, EnemyStateMachine.ChaseCollider.radius);
            if (!result) return;
            EnemyStateMachine.SwitchState(new SkeletonChaseState(EnemyStateMachine));
        }
    }
}