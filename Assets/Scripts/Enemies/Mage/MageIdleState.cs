using CustomInterfaces;
using Tools;
using UnityEngine;
using Zenject;

namespace Enemies.Mage
{
    public class MageIdleState : MageBaseState
    {
        private readonly int _idleAnimationHash = Animator.StringToHash("Mage_Idle");
        private GenericTimer _genericTimer;

        public MageIdleState(MageStateMachine mageStateMachine, IInstantiator instantiator) : base(mageStateMachine, instantiator){}
        
        public override void OnEnter()
        {
            _genericTimer = Instantiator.Instantiate<GenericTimer>(new object[]{3});
            _genericTimer.OnTimerFinished += CheckIdleWaitFinished;

            MageStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            
            MageStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            DrawChaseOverlayAndCheck();
        }

        public override void OnExit()
        {
            MageStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
            _genericTimer.OnTimerFinished -= CheckIdleWaitFinished;
        }
        
        private void CheckIdleWaitFinished()
        {
            MageStateMachine.SwitchState(MageStateMachine.MagePatrolState);
        }
        
        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            MageStateMachine.SwitchState(MageStateMachine.MageHurtState);
        }
        
        private void DrawChaseOverlayAndCheck()
        {
            var results = Physics2D.OverlapCircleAll(MageStateMachine.ChaseCollider.transform.position, MageStateMachine.ChaseCollider.radius);
            
            foreach (var result in results)
            {
                if (!result) continue;

                if (!result.TryGetComponent(out IPlayer player))
                    player = result.GetComponentInParent<IPlayer>();

                if (player != null)
                {
                    MageStateMachine.MageChaseState.Init(player);
                    MageStateMachine.SwitchState(MageStateMachine.MageChaseState);
                }
            }
        }
    }
}