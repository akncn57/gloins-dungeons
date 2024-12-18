using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Player.States
{
    public class PlayerDeathState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Death;
        
        private readonly int _deathAnimationHash = Animator.StringToHash("Warrior_Death");
        
        public PlayerDeathState(
            PlayerStateMachine playerStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(playerStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}

        public override void OnEnter()
        {
            Debug.Log("Player Death!");
            
            ICommand stopCommand = new PlayerStopMoveCommand(PlayerStateMachine.PlayerMover, PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopCommand);
            
            PlayerStateMachine.Animator.Play("Death-BlendTree");
        }

        public override void OnTick(){}

        public override void OnExit(){}
    }
}