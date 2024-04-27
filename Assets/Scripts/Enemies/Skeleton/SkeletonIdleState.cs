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
            //TODO: Patrol list reverse edilince ayni pozisyonda 2 kere bekliyor.
            _genericTimer = Instantiator.Instantiate<GenericTimer>(new object[]{3});
            
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            _genericTimer.OnTimerFinished += CheckIdleWaitFinished;
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            DrawChaseOverlayAndCheck();
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
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonPatrolState);
        }
        
        private void DrawChaseOverlayAndCheck()
        {
            var results = Physics2D.OverlapCircleAll(SkeletonStateMachine.ChaseCollider.transform.position, SkeletonStateMachine.ChaseCollider.radius);
            
            foreach (var result in results)
            {
                if (!result) continue;
                
                if (result.CompareTag("Player"))
                {
                    SkeletonStateMachine.SkeletonChaseState.Init(result.gameObject);
                    SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonChaseState);
                }
            }
        }
    }
}