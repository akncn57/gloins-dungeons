using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonPatrolState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        
        public SkeletonPatrolState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}
        
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