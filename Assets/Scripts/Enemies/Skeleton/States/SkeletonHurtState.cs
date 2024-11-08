using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonHurtState : SkeletonBaseState
    {
        private readonly int _hurtAnimationHash = Animator.StringToHash("Skeleton_Hurt");
        
        public SkeletonHurtState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

        public override void OnEnter()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart += EnemyOnHurtStart;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd += EnemyOnHurtEnd;
            
            ICommand stopMoveCommand = new EnemyStopMovementCommand(
                SkeletonStateMachine.EnemyStopMovement, 
                SkeletonStateMachine.EnemyNavMeshAgent);
            CommandInvoker.ExecuteCommand(stopMoveCommand);
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_hurtAnimationHash, 0.1f);
            
            SkeletonStateMachine.HurtParticle.Play();
        }

        public override void OnTick()
        {
            CheckDeath();
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart -= EnemyOnHurtStart;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd -= EnemyOnHurtEnd;
        }

        private void EnemyOnHurtStart()
        {
            ICommand spendHealthCommand = new EnemySpendHealthCommand(
                SkeletonStateMachine.HealthController,
                SkeletonStateMachine.HitData.Damage);
            CommandInvoker.ExecuteCommand(spendHealthCommand);
            
            Debug.Log("Enemy Skeleton Health : " + SkeletonStateMachine.HealthController.HealthData.Health);
            
            // ICommand knockBackCommand = new EnemyKnockBackCommand(
            //     SkeletonStateMachine.EnemyMover,
            //     SkeletonStateMachine.Rigidbody,
            //     SkeletonStateMachine.HitData.HitPosition.x,
            //     SkeletonStateMachine.HitData.KnockBackStrength);
            // CommandInvoker.ExecuteCommand(knockBackCommand);
        }

        private void EnemyOnHurtEnd()
        {
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
        }
        
        private void CheckDeath()
        {
            if (SkeletonStateMachine.HealthController.HealthData.Health <= 0) SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonDeathState);
        }
    }
}