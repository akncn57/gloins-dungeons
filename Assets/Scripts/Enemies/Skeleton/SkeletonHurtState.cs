using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonHurtState : EnemyBaseState
    {
        private readonly int _hurtAnimationHash = Animator.StringToHash("Skeleton_Hurt");
        
        public SkeletonHurtState(EnemyBaseStateMachine enemyStateMachine) : base(enemyStateMachine){}

        public override void OnEnter()
        {
            EnemyStateMachine.EnemyColliderController.OnHitEnd += HurtEnd;
            
            EnemyStateMachine.Rigidbody.velocity = Vector2.zero;
            EnemyStateMachine.Animator.CrossFadeInFixedTime(_hurtAnimationHash, 0.1f);
            
            EnemyStateMachine.HurtParticle.Play();
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            EnemyStateMachine.EnemyColliderController.OnHitEnd -= HurtEnd;
        }

        private void HurtEnd()
        {
            EnemyStateMachine.SwitchState(new SkeletonIdleState(EnemyStateMachine));
        }
    }
}