using UnityEngine;

namespace Player
{
    public class PlayerAttackBasicState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.AttackBasic;
        
        private readonly int _attackBasicAnimationHash = Animator.StringToHash("Player_Attack_Basic");
        
        public PlayerAttackBasicState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicColliderOpen += AttackBasicOpenCollider;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicColliderClose += AttackBasicCloseCollider;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicFinished += AttackBasicFinish;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            
            PlayerStateMachine.RigidBody.velocity = Vector2.zero;
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_attackBasicAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicColliderOpen -= AttackBasicOpenCollider;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicColliderClose -= AttackBasicCloseCollider;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnAttackBasicFinished -= AttackBasicFinish;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
        }

        private void AttackBasicOpenCollider()
        {
            PlayerStateMachine.AttackBasicColliderObject.SetActive(true);
        }
        
        private void AttackBasicCloseCollider()
        {
            PlayerStateMachine.AttackBasicColliderObject.SetActive(false);
        }

        private void AttackBasicFinish()
        {
            PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }
        
        private void CheckOnHurt(Vector3 hitPosition)
        {
            PlayerStateMachine.SwitchState(new PlayerHurtState(PlayerStateMachine, hitPosition));
        }
    }
}