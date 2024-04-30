using System.Collections.Generic;
using ColliderController;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonAttackBasicState : SkeletonBaseState
    {
        private readonly int _attackBasicAnimationHash = Animator.StringToHash("Skeleton_Attack_Basic");
        
        private readonly List<ColliderControllerBase> _hittingEnemies = new();
        
        protected SkeletonAttackBasicState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

        public override void OnEnter()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapOpen += SkeletonOnAttackBasicOpenOverlap;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapClose += SkeletonOnAttackBasicCloseOverlap;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished += SkeletonOnAttackBasicFinish;
            
            SkeletonStateMachine.Rigidbody.velocity = Vector2.zero;
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_attackBasicAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapOpen -= SkeletonOnAttackBasicOpenOverlap;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapClose -= SkeletonOnAttackBasicCloseOverlap;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished -= SkeletonOnAttackBasicFinish;
        }

        private void SkeletonOnAttackBasicOpenOverlap()
        {
            var results = Physics2D.OverlapCapsuleAll(SkeletonStateMachine.AttackBasicCollider.transform.position, SkeletonStateMachine.AttackBasicCollider.size,
                SkeletonStateMachine.AttackBasicCollider.direction, 0f);
            
            _hittingEnemies.Clear();
            
            foreach (var result in results)
            {
                if (!result) continue;
                var enemy = result.GetComponent<ColliderControllerBase>();
                _hittingEnemies.Add(enemy);
                enemy.InvokeOnHitStartEvent(10, (enemy.transform.position - SkeletonStateMachine.transform.position).normalized, 2f);
            }
        }
        
        private void SkeletonOnAttackBasicCloseOverlap()
        {
            foreach (var enemy in _hittingEnemies)
            {
                if (!enemy) continue;
                enemy.InvokeOnHitEndEvent();
            }
            _hittingEnemies.Clear();
        }
        
        private void SkeletonOnAttackBasicFinish()
        {
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
        }
    }
}