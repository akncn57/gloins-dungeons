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
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished += SkeletonOnAttackBasicFinish;
            
            SkeletonStateMachine.Rigidbody.velocity = Vector2.zero;
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_attackBasicAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished -= SkeletonOnAttackBasicFinish;
        }
        
        private void SkeletonOnAttackBasicFinish()
        {
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
        }
    }
}