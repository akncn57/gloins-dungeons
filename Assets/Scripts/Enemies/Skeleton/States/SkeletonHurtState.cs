using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
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
            
            ICommand stopMoveCommand = new EnemyStopMoveCommand(SkeletonStateMachine.EnemyMover, SkeletonStateMachine.Rigidbody);
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
            SkeletonStateMachine.HealthController.SpendHealth(SkeletonStateMachine.HitData.Damage);
            Debug.Log("Enemy Skeleton Health : " + SkeletonStateMachine.HealthController.HealthData.Health);
            SkeletonStateMachine.Rigidbody.velocity = new Vector2(SkeletonStateMachine.HitData.HitPosition.x * SkeletonStateMachine.HitData.KnockBackStrength, SkeletonStateMachine.Rigidbody.velocity.y);
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