using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonChaseState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        
        public SkeletonChaseState(SkeletonStateMachine enemyStateMachine) : base(enemyStateMachine)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Enemy Chase State");
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}