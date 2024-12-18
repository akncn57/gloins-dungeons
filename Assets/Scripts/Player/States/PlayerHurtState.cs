using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Player.States
{
    public class PlayerHurtState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Hurt;
        
        private readonly int _hurtAnimationHash = Animator.StringToHash("Warrior_Hurt");

        public PlayerHurtState(
            PlayerStateMachine playerStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(playerStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnHurtStart += PlayerOnHurtStart;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnHurtEnd += PlayerOnHurtEnd;
            
            ICommand stopCommand = new PlayerStopMoveCommand(PlayerStateMachine.PlayerMover, PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopCommand);
            
            PlayerStateMachine.HurtParticle.Play();
            PlayerStateMachine.Animator.Play("Hurt-BlendTree");
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

            ICommand spendHealthCommand = new PlayerHurtCommand(PlayerStateMachine.HealthController, PlayerStateMachine.HitData.Damage);
            CommandInvoker.ExecuteCommand(spendHealthCommand);
            
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