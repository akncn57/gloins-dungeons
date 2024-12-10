using System.Collections;
using DesignPatterns.CommandPattern;
using Player.Commands;
using Tools;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Player.States
{
    public class PlayerDashState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum { get; }
        
        private readonly int _dashAnimationHash = Animator.StringToHash("Warrior_Dash");
        private GenericTimer _genericTimer;

        public PlayerDashState(
            PlayerStateMachine playerStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(playerStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}

        public override void OnEnter()
        {
            // PlayerStateMachine.TrailRenderer.enabled = true;
            
            _genericTimer = Instantiator.Instantiate<GenericTimer>(new object[]{PlayerStateMachine.PlayerProperties.DashTime});
            _genericTimer.OnTimerFinished += CheckDashTimeFinish;
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_dashAnimationHash, 0.1f);
            
            ICommand dashCommand = new PlayerDashCommand(
                PlayerStateMachine.PlayerMover, 
                PlayerStateMachine.RigidBody,
                PlayerStateMachine.InputReader.MovementValue, 
                PlayerStateMachine.PlayerProperties.DashForce);
            CommandInvoker.ExecuteCommand(dashCommand);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            _genericTimer.OnTimerFinished -= CheckDashTimeFinish;
        }

        private void CheckDashTimeFinish()
        {
            // PlayerStateMachine.TrailRenderer.enabled = false;
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerIdleState);
        }
    }
}