using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonIdleState : EnemyBaseState
    {
        private readonly int _idleAnimationHash = Animator.StringToHash("Skeleton_Idle");
        
        public SkeletonIdleState(EnemyBaseStateMachine skeletonStateMachine) : base(skeletonStateMachine){}
        
        public override void OnEnter()
        {
            EnemyStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            
            EnemyStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            //DrawChaseOverlayAndCheck();
        }

        public override void OnExit()
        {
            EnemyStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
        }

        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            EnemyStateMachine.SwitchState(new SkeletonHurtState(EnemyStateMachine, knockBackPosition, 50, knockBackPower));
        }
        
        private void DrawChaseOverlayAndCheck()
        {
            var result = Physics2D.OverlapCircle(EnemyStateMachine.ChaseCollider.transform.position, EnemyStateMachine.ChaseCollider.radius);
            if (!result) return;
            EnemyStateMachine.SwitchState(new SkeletonChaseState(EnemyStateMachine));
        }
    }
}