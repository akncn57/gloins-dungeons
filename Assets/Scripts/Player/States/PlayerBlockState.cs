using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;
using Zenject;

namespace Player.States
{
    public class PlayerBlockState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Block;
        
        private readonly int _idleAnimationHash = Animator.StringToHash("Warrior_Idle");
        
        public PlayerBlockState(PlayerStateMachine playerStateMachine, IInstantiator instantiator) : base(playerStateMachine, instantiator){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart += CheckOnHurt;
            
            PlayerStateMachine.ShieldObject.SetActive(true);
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            ICommand stopMoveCommand = new PlayerStopMoveCommand(PlayerStateMachine.PlayerMover, PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopMoveCommand);
            
            CheckBlockingFinished();
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart -= CheckOnHurt;
            
            PlayerStateMachine.ShieldObject.SetActive(false);
        }

        private void CheckBlockingFinished()
        {
            if (!PlayerStateMachine.InputReader.IsBlocking)
                PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerIdleState);
        }
        
        private void CheckOnHurt(int damage, Vector3 hitPosition, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerHurtState);
        }
    }
}