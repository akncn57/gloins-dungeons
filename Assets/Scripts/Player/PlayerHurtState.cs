using UnityEngine;

namespace Player
{
    public class PlayerHurtState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Hurt;
        
        private readonly int _hurtAnimationHash = Animator.StringToHash("Player_Hurt");
        private Vector3 _hitPosition;
        private int _damage;
        private float _knockBackStrength;

        public PlayerHurtState(
            PlayerStateMachine playerStateMachine,
            Vector3 hitPosition,
            int damage,
            float knockBackStrength) : base(playerStateMachine)
        {
            _hitPosition = hitPosition;
            _damage = damage;
            _knockBackStrength = knockBackStrength;
        }

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnHurtStart += PlayerOnHurtStart;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnHurtEnd += PlayerOnHurtEnd;
            
            PlayerStateMachine.RigidBody.velocity = Vector2.zero;
            PlayerStateMachine.HurtParticle.Play();
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_hurtAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            CheckDeath();
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnHurtStart -= PlayerOnHurtStart;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnHurtEnd -= PlayerOnHurtEnd;
        }

        private void PlayerOnHurtStart()
        {
            PlayerStateMachine.HealthController.SpendHealth(_damage);
            Debug.Log("Player Health : " + PlayerStateMachine.HealthController.Health);
            PlayerStateMachine.RigidBody.velocity = new Vector2(_hitPosition.x * _knockBackStrength, PlayerStateMachine.RigidBody.velocity.y);
        }
        
        private void PlayerOnHurtEnd()
        {
            PlayerStateMachine.SwitchState(new PlayerIdleState(PlayerStateMachine));
        }

        private void CheckDeath()
        {
            if (PlayerStateMachine.HealthController.Health <= 0) PlayerStateMachine.SwitchState(new PlayerDeathState(PlayerStateMachine));
        }
    }
}