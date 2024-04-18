using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonPatrolState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        
        public SkeletonPatrolState(SkeletonStateMachine enemyStateMachine) : base(enemyStateMachine)
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