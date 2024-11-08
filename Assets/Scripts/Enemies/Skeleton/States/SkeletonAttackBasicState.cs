using System.Collections.Generic;
using ColliderController;
using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies.Skeleton.States
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
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;

            ICommand stopMoveCommand = new EnemyStopMovementCommand(
                SkeletonStateMachine.EnemyStopMovement, 
                SkeletonStateMachine.EnemyNavMeshAgent);
            CommandInvoker.ExecuteCommand(stopMoveCommand);
            
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
            SkeletonStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
        }

        private void SkeletonOnAttackBasicOpenOverlap()
        {
            var results = Physics2D.OverlapCapsuleAll(
                SkeletonStateMachine.AttackBasicCollider.transform.position,
                SkeletonStateMachine.AttackBasicCollider.size,
                SkeletonStateMachine.AttackBasicCollider.direction,
                0f);
            
            _hittingEnemies.Clear();
            
            foreach (var result in results)
            {
                if (result.TryGetComponent<ColliderControllerBase>(out var colliderController))
                {
                    _hittingEnemies.Add(colliderController);
                    
                    colliderController.InvokeOnHitStartEvent(
                        10,
                        (colliderController.transform.position - SkeletonStateMachine.transform.position).normalized,
                        2f);
                }
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
        
        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            if (!SkeletonStateMachine.IsBlocking)
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonHurtState);
        }
    }
}