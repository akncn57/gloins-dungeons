using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonDeathState : SkeletonBaseState
    {
        private readonly int _deathAnimationHash = Animator.StringToHash("Skeleton_Death");
        
        public SkeletonDeathState(SkeletonStateMachine enemyStateMachine) : base(enemyStateMachine){}

        public override void OnEnter()
        {
            Debug.Log("Enemy Skeleton Death!");
            SkeletonStateMachine.Collider.enabled = false;
            SkeletonStateMachine.Rigidbody.velocity = Vector2.zero;
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_deathAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}