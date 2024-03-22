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
            
        }

        public override void OnExit()
        {
            EnemyStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
        }

        private void CheckOnHurt(int damage)
        {
            EnemyStateMachine.SwitchState(new SkeletonHurtState(EnemyStateMachine));
        }
    }
}