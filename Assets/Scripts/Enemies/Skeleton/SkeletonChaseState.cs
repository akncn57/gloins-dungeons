using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonChaseState : EnemyBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        
        public SkeletonChaseState(EnemyBaseStateMachine enemyStateMachine) : base(enemyStateMachine)
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