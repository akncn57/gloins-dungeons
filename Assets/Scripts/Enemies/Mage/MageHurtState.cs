using UnityEngine;
using Zenject;

namespace Enemies.Mage
{
    public class MageHurtState : MageBaseState
    {
        private readonly int _hurtAnimationHash = Animator.StringToHash("Mage_Hurt");
        
        public MageHurtState(MageStateMachine mageStateMachine, IInstantiator instantiator) : base(mageStateMachine, instantiator){}
        
        public override void OnEnter()
        {
            MageStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart += EnemyOnHurtStart;
            MageStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd += EnemyOnHurtEnd;
            
            MageStateMachine.Rigidbody.linearVelocity = Vector2.zero;
            MageStateMachine.Animator.CrossFadeInFixedTime(_hurtAnimationHash, 0.1f);
            
            MageStateMachine.HurtParticle.Play();
        }

        public override void OnTick()
        {
            CheckDeath();
        }

        public override void OnExit()
        {
            MageStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart -= EnemyOnHurtStart;
            MageStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd -= EnemyOnHurtEnd;
        }
        
        private void EnemyOnHurtStart()
        {
            MageStateMachine.HealthController.SpendHealth(MageStateMachine.HitData.Damage);
            Debug.Log("Enemy Mage Health : " + MageStateMachine.HealthController.HealthData.Health);
            MageStateMachine.Rigidbody.linearVelocity = new Vector2(MageStateMachine.HitData.HitPosition.x * MageStateMachine.HitData.KnockBackStrength, MageStateMachine.Rigidbody.linearVelocity.y);
        }

        private void EnemyOnHurtEnd()
        {
            MageStateMachine.SwitchState(MageStateMachine.MageIdleState);
        }
        
        private void CheckDeath()
        {
            if (MageStateMachine.HealthController.HealthData.Health <= 0) MageStateMachine.SwitchState(MageStateMachine.MageDeathState);
        }
    }
}