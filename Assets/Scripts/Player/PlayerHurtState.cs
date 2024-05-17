using UnityEngine;

namespace Player
{
    public class PlayerHurtState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Hurt;
        
        private readonly int _hurtAnimationHash = Animator.StringToHash("Player_Hurt");

        public PlayerHurtState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

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
            PlayerStateMachine.HealthController.SpendHealth(PlayerStateMachine.HitData.Damage);
            Debug.Log("Player Health : " + PlayerStateMachine.HealthController.HealthData.Health);
            PlayerStateMachine.RigidBody.velocity = new Vector2(PlayerStateMachine.HitData.HitPosition.x * PlayerStateMachine.HitData.KnockBackStrength, PlayerStateMachine.RigidBody.velocity.y);
        }
        
        private void PlayerOnHurtEnd()
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerIdleState);
        }

        private void CheckDeath()
        {
            if (PlayerStateMachine.HealthController.HealthData.Health <= 0) PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerDeathState);
        }
    }
}