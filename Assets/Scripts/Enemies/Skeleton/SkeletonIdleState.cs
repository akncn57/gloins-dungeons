using Tools;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton
{
    public class SkeletonIdleState : SkeletonBaseState
    {
        private readonly int _idleAnimationHash = Animator.StringToHash("Skeleton_Idle");
        private GenericTimer _genericTimer;
        
        public SkeletonIdleState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}
        
        public override void OnEnter()
        {
            _genericTimer = Instantiator.Instantiate<GenericTimer>(new object[]{5});
            
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            _genericTimer.OnTimerFinished += CheckIdleWaitFinished;
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            //DrawChaseOverlayAndCheck();
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
            _genericTimer.OnTimerFinished -= CheckIdleWaitFinished;
        }

        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonHurtState);
        }

        private void CheckIdleWaitFinished()
        {
            Debug.Log("Skeleton enter Patrol State!");
        }
        
        // private void DrawChaseOverlayAndCheck()
        // {
        //     var result = Physics2D.OverlapCircle(EnemyStateMachine.ChaseCollider.transform.position, EnemyStateMachine.ChaseCollider.radius);
        //     if (!result) return;
        //     EnemyStateMachine.SwitchState(new SkeletonChaseState(EnemyStateMachine));
        // }
    }
}