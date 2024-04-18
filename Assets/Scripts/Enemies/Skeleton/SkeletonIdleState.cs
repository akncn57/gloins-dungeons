using UnityEngine;

namespace Enemies.Skeleton
{
    public class SkeletonIdleState : SkeletonBaseState
    {
        private readonly int _idleAnimationHash = Animator.StringToHash("Skeleton_Idle");
        
        public SkeletonIdleState(SkeletonStateMachine skeletonStateMachine) : base(skeletonStateMachine){}
        
        public override void OnEnter()
        {
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            //DrawChaseOverlayAndCheck();
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
        }

        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonHurtState);
        }
        
        // private void DrawChaseOverlayAndCheck()
        // {
        //     var result = Physics2D.OverlapCircle(EnemyStateMachine.ChaseCollider.transform.position, EnemyStateMachine.ChaseCollider.radius);
        //     if (!result) return;
        //     EnemyStateMachine.SwitchState(new SkeletonChaseState(EnemyStateMachine));
        // }
    }
}