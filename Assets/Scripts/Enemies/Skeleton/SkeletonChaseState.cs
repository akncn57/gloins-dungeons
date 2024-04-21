using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonChaseState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        
        public SkeletonChaseState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

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