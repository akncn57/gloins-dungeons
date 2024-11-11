using System.Collections.Generic;
using ColliderController;
using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonAttackHeavyState : SkeletonBaseState
    {
        private readonly int _attackHeavyAnimationHash = Animator.StringToHash("Skeleton_Attack_Heavy");
        private readonly List<ColliderControllerBase> _hittingEnemies = new();

        
        protected SkeletonAttackHeavyState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator) {}
        
        public override void OnEnter()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackHeavyOverlapOpen += SkeletonOnAttackHeavyOpenOverlap;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackHeavyOverlapClose += SkeletonOnAttackHeavyCloseOverlap;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackHeavyFinished += SkeletonOnAttackHeavyFinish;
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            
            ICommand stopMoveCommand = new EnemyStopMovementCommand(
                SkeletonStateMachine.EnemyStopMovement, 
                SkeletonStateMachine.EnemyNavMeshAgent);
            CommandInvoker.ExecuteCommand(stopMoveCommand);
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_attackHeavyAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackHeavyOverlapOpen -= SkeletonOnAttackHeavyOpenOverlap;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackHeavyOverlapClose -= SkeletonOnAttackHeavyCloseOverlap;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackHeavyFinished -= SkeletonOnAttackHeavyFinish;
            SkeletonStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
        }

        private void SkeletonOnAttackHeavyOpenOverlap()
        {
            var results = Physics2D.OverlapBoxAll(
                SkeletonStateMachine.AttackHeavyCollider.transform.position,
                SkeletonStateMachine.AttackHeavyCollider.size,
                0f);
            
            _hittingEnemies.Clear();
            
            foreach (var result in results)
            {
                if (result.TryGetComponent<ColliderControllerBase>(out var colliderController))
                {
                    _hittingEnemies.Add(colliderController);
                    
                    colliderController.InvokeOnHitStartEvent(
                        SkeletonStateMachine.EnemyProperties.HeavyAttackPower,
                        (colliderController.transform.position - SkeletonStateMachine.transform.position).normalized,
                        SkeletonStateMachine.EnemyProperties.HitKnockBackPower);
                }
            }
        }

        private void SkeletonOnAttackHeavyCloseOverlap()
        {
            foreach (var enemy in _hittingEnemies)
            {
                if (!enemy) continue;
                enemy.InvokeOnHitEndEvent();
            }
            _hittingEnemies.Clear();
        }

        private void SkeletonOnAttackHeavyFinish()
        {
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
        }

        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            if (!SkeletonStateMachine.IsBlocking)
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonHurtState);
        }
    }
}