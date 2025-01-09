using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcHurtState : OrcBaseState
    {
        protected OrcHurtState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        public override void OnEnter()
        {
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart += HurtStart;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd += HurtEnd;
            
            OrcStateMachine.Animator.Play("Hurt-BlendTree");
            OrcStateMachine.HurtParticle.Play();
        }

        public override void OnTick()
        {
            CheckDeath();
        }

        public override void OnExit()
        {
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart -= HurtStart;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd -= HurtEnd;
        }
        
        private void HurtStart()
        {
            ICommand spendHealthCommand = new EnemySpendHealthCommand(
                OrcStateMachine.HealthController,
                OrcStateMachine.HitData.Damage);
            CommandInvoker.ExecuteCommand(spendHealthCommand);
            
            Debug.Log("Orc Health : " + OrcStateMachine.HealthController.HealthData.Health);
            
            ICommand knockBackCommand = new EnemyKnockbackCommand(
                OrcStateMachine.EnemyKnockback,
                OrcStateMachine.Rigidbody,
                OrcStateMachine.HitData.HitPosition.x,
                OrcStateMachine.HitData.HitPosition.y,
                OrcStateMachine.HitData.KnockBackStrength);
            CommandInvoker.ExecuteCommand(knockBackCommand);
        }
        
        private void HurtEnd()
        {
            OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
        }
        
        private void CheckDeath()
        {
            //TODO: Switch the Death State.
            // if (OrcStateMachine.HealthController.HealthData.Health <= 0) OrcStateMachine.SwitchState(OrcStateMachine.);
        }
    }
}