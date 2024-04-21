using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonDeathState : SkeletonBaseState
    {
        private readonly int _deathAnimationHash = Animator.StringToHash("Skeleton_Death");
        
        public SkeletonDeathState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

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