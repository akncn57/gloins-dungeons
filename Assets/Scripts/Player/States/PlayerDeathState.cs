using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;
using Zenject;

namespace Player.States
{
    public class PlayerDeathState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Death;
        
        private readonly int _deathAnimationHash = Animator.StringToHash("Warrior_Death");
        
        public PlayerDeathState(PlayerStateMachine playerStateMachine, IInstantiator instantiator, SignalBus signalBus) : base(playerStateMachine, instantiator, signalBus){}

        public override void OnEnter()
        {
            Debug.Log("Player Death!");
            
            ICommand stopCommand = new PlayerStopMoveCommand(PlayerStateMachine.PlayerMover, PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopCommand);
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_deathAnimationHash, 0.1f);
        }

        public override void OnTick(){}

        public override void OnExit(){}
    }
}