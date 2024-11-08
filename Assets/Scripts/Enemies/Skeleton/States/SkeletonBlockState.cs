using Tools;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonBlockState : SkeletonBaseState
    {
        private readonly int _blockUpAnimationHash = Animator.StringToHash("Skeleton_BlockUp");
        private readonly int _blockDownAnimationHash = Animator.StringToHash("Skeleton_BlockDown");
        private GenericTimer _genericTimer;
        
        protected SkeletonBlockState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator) {}

        public override void OnEnter()
        {
            _genericTimer = Instantiator.Instantiate<GenericTimer>(new object[]{SkeletonStateMachine.EnemyProperties.BlockDuration});
            _genericTimer.OnTimerFinished += CheckBlockTimeFinished;

            SkeletonStateMachine.IsBlocking = true;
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_blockUpAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            _genericTimer.OnTimerFinished -= CheckBlockTimeFinished;
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_blockDownAnimationHash, 0.1f);
        }
        
        private void CheckBlockTimeFinished()
        {
            SkeletonStateMachine.IsBlocking = false;
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
        }
    }
}