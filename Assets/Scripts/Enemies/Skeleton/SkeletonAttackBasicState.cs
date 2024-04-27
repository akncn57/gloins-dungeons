using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonAttackBasicState : SkeletonBaseState
    {
        private readonly int _attackBasicAnimationHash = Animator.StringToHash("Skeleton_Attack_Basic");

        protected SkeletonAttackBasicState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

        public override void OnEnter()
        {
            SkeletonStateMachine.Rigidbody.velocity = Vector2.zero;
            
            Debug.Log("Enemy Attack Basic State");
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}