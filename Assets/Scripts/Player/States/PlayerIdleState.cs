using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;
using Zenject;

namespace Player.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Idle;
        
        private readonly int _idleAnimationHash = Animator.StringToHash("Warrior_Idle");
        
        public PlayerIdleState(PlayerStateMachine playerStateMachine, IInstantiator instantiator, SignalBus signalBus) : base(playerStateMachine, instantiator, signalBus){}

        public override void OnEnter()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent += CheckAttackBasic;
            PlayerStateMachine.InputReader.AttackHeavyEvent += CheckAttackHeavy;
            PlayerStateMachine.InputReader.DashEvent += CheckDash;
            PlayerStateMachine.PlayerColliderController.OnHitStart += CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart += CheckOnHurt;
            
            ICommand stopMoveCommand = new PlayerStopMoveCommand(
                PlayerStateMachine.PlayerMover, 
                PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopMoveCommand);
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            CheckWalking();
            CheckBlocking();
        }

        public override void OnExit()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent -= CheckAttackBasic;
            PlayerStateMachine.InputReader.AttackHeavyEvent -= CheckAttackHeavy;
            PlayerStateMachine.InputReader.DashEvent -= CheckDash;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart -= CheckOnHurt;
        }
        
        private void CheckWalking()
        {
            if (PlayerStateMachine.InputReader.MovementValue != Vector2.zero)
                PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerWalkState);
        }

        private void CheckBlocking()
        {
            if (PlayerStateMachine.InputReader.IsBlocking)
                PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerBlockState);
        }
        
        private void CheckAttackBasic()
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerAttackBasicState);
        }
        
        private void CheckAttackHeavy()
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerAttackHeavyState);
        }
        
        private void CheckDash()
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerDashState);
        }

        private void CheckOnHurt(int damage, Vector3 hitPosition, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerHurtState);
        }
    }
}