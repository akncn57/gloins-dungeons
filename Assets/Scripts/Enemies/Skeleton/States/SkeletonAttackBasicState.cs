using System.Collections.Generic;
using ColliderController;
using DesignPatterns.CommandPattern;
using Enemies.Commands;
using EventInterfaces;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonAttackBasicState : SkeletonBaseState
    {
        private readonly int _attackBasicAnimationHash = Animator.StringToHash("Skeleton_Attack_Basic");
        
        private readonly List<ColliderControllerBase> _hittingEnemies = new();
        
        protected SkeletonAttackBasicState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator, SignalBus signalBus) : base(skeletonStateMachine, instantiator, signalBus){}

        public override void OnEnter()
        {
            SignalBus.Subscribe<IPlayerEvents.OnPlayerAttacked>(CheckPlayerAttack);
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
            SignalBus.Unsubscribe<IPlayerEvents.OnPlayerAttacked>(CheckPlayerAttack);
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
                        SkeletonStateMachine.EnemyProperties.BasicAttackPower,
                        (colliderController.transform.position - SkeletonStateMachine.transform.position).normalized,
                        SkeletonStateMachine.EnemyProperties.HitKnockBackPower);
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
        
        private void CheckPlayerAttack()
        {
            if (!SkeletonStateMachine.IsEnemyNearToPlayer) return;
            
            var randomBlockChange = Random.Range(0f, 1f);
            
            if (randomBlockChange <= SkeletonStateMachine.EnemyProperties.BlockChance)
            {
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonBlockState);
            }
        }
    }
}