using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonPatrolState : EnemyBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        private readonly int _idleAnimationHash = Animator.StringToHash("Skeleton_Idle");
        
        public SkeletonPatrolState(EnemyBaseStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void OnEnter()
        {
            
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}