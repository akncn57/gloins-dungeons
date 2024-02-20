using UnityEngine;

namespace Player
{
    public class PlayerHurtState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Hurt;
        
        private readonly int _hurtAnimationHash = Animator.StringToHash("Player_Hurt");
        private Vector3 _hitPosition;
        private float _knockBackStrength;

        public PlayerHurtState(
            PlayerStateMachine playerStateMachine,
            Vector3 hitPosition,
            float knockBackStrength) : base(playerStateMachine)
        {
            _hitPosition = hitPosition;
            _knockBackStrength = knockBackStrength;
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
            PlayerStateMachine.RigidBody.velocity = new Vector2(_hitPosition.x * _knockBackStrength, PlayerStateMachine.RigidBody.velocity.y);
        }
        
        private void HurtEnd()
        {
            PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }
    }
}