using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonHurtState : EnemyBaseState
    {
        private readonly int _hurtAnimationHash = Animator.StringToHash("Skeleton_Hurt");
        private Vector3 _hitPosition;
        private int _damage;
        private float _knockBackStrength;
        
        public SkeletonHurtState(
            EnemyBaseStateMachine enemyStateMachine,
            Vector3 hitPosition,
            int damage,
            float knockBackStrength) : base(enemyStateMachine)
        {
            _hitPosition = hitPosition;
            _damage = damage;
            _knockBackStrength = knockBackStrength;
        }

        public override void OnEnter()
        {
            EnemyStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart += EnemyOnHurtStart;
            EnemyStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd += EnemyOnHurtEnd;
            
            EnemyStateMachine.Rigidbody.velocity = Vector2.zero;
            EnemyStateMachine.Animator.CrossFadeInFixedTime(_hurtAnimationHash, 0.1f);
            
            EnemyStateMachine.HurtParticle.Play();
        }

        public override void OnTick()
        {
            CheckDeath();
        }

        public override void OnExit()
        {
            EnemyStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart -= EnemyOnHurtStart;
            EnemyStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd -= EnemyOnHurtEnd;
        }

        private void EnemyOnHurtStart()
        {
            EnemyStateMachine.HealthController.SpendHealth(_damage);
            Debug.Log("Enemy Skeleton Health : " + EnemyStateMachine.HealthController.Health);
            EnemyStateMachine.Rigidbody.velocity = new Vector2(_hitPosition.x * _knockBackStrength, EnemyStateMachine.Rigidbody.velocity.y);
        }

        private void EnemyOnHurtEnd()
        {
            EnemyStateMachine.SwitchState(new SkeletonIdleState(EnemyStateMachine));
        }
        
        private void CheckDeath()
        {
            if (EnemyStateMachine.HealthController.Health <= 0) EnemyStateMachine.SwitchState(new SkeletonDeathState(EnemyStateMachine));
        }
    }
}