using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonHurtState : SkeletonBaseState
    {
        private readonly int _hurtAnimationHash = Animator.StringToHash("Skeleton_Hurt");
        
        public SkeletonHurtState(SkeletonStateMachine enemyStateMachine) : base(enemyStateMachine){}

        public override void OnEnter()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart += EnemyOnHurtStart;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd += EnemyOnHurtEnd;
            
            SkeletonStateMachine.Rigidbody.velocity = Vector2.zero;
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_hurtAnimationHash, 0.1f);
            
            SkeletonStateMachine.HurtParticle.Play();
        }

        public override void OnTick()
        {
            CheckDeath();
            Debug.Log("sa");
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtStart -= EnemyOnHurtStart;
            SkeletonStateMachine.EnemyAnimationEventTrigger.EnemyOnHurtEnd -= EnemyOnHurtEnd;
        }

        private void EnemyOnHurtStart()
        {
            SkeletonStateMachine.HealthController.SpendHealth(SkeletonStateMachine.HitData.Damage);
            Debug.Log("Enemy Skeleton Health : " + SkeletonStateMachine.HealthController.Health);
            SkeletonStateMachine.Rigidbody.velocity = new Vector2(SkeletonStateMachine.HitData.HitPosition.x * SkeletonStateMachine.HitData.KnockBackStrength, SkeletonStateMachine.Rigidbody.velocity.y);
        }

        private void EnemyOnHurtEnd()
        {
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
        }
        
        private void CheckDeath()
        {
            if (SkeletonStateMachine.HealthController.Health <= 0) SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonDeathState);
        }
    }
}