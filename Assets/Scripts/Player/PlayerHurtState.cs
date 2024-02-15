using UnityEngine;

namespace Player
{
    public class PlayerHurtState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Hurt;
        
        private readonly int _hurtAnimationHash = Animator.StringToHash("Player_Hurt");
        private Vector3 _hitPosition;

        public PlayerHurtState(PlayerStateMachine playerStateMachine, Vector3 hitPosition) : base(playerStateMachine)
        {
            _hitPosition = hitPosition;
        }

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnHurtStart += HurtStart;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnHurtEnd += HurtEnd;
            
            PlayerStateMachine.RigidBody.velocity = Vector2.zero;
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_hurtAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnHurtStart -= HurtStart;
            PlayerStateMachine.PlayerAnimationEventsTrigger.OnHurtEnd -= HurtEnd;
        }

        private void HurtStart()
        {
            PlayerStateMachine.RigidBody.velocity = new Vector2(-_hitPosition.x, PlayerStateMachine.RigidBody.velocity.y);
        }
        
        private void HurtEnd()
        {
            PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }
    }
}