using UnityEngine;
using DesignPatterns.CommandPattern;
using Player.Commands;

namespace Player
{
    public class PlayerHurtState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Hurt;
        
        private readonly int _hurtAnimationHash = Animator.StringToHash("Warrior_Hurt");

        public PlayerHurtState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnHurtStart += PlayerOnHurtStart;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnHurtEnd += PlayerOnHurtEnd;
            
            ICommand stopCommand = new PlayerStopMoveCommand(PlayerStateMachine.PlayerMover, PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopCommand);
            
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

            ICommand knockBackCommand = new PlayerKnockBackCommand(
                PlayerStateMachine.PlayerMover,
                PlayerStateMachine.RigidBody,
                PlayerStateMachine.HitData.HitPosition.x,
                PlayerStateMachine.HitData.KnockBackStrength);
            CommandInvoker.ExecuteCommand(knockBackCommand);
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