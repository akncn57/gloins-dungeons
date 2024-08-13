using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;

namespace Player
{
    public class PlayerWalkState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Walk;
        
        private readonly int _walkAnimationHash = Animator.StringToHash("Warrior_Walk");
        
        public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent += CheckAttackBasic;
            PlayerStateMachine.PlayerColliderController.OnHitStart += CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart += CheckOnHurt;
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            CheckStopMoving();
            CheckBlocking();

            ICommand runCommand = new PlayerMoveCommand(
                PlayerStateMachine.PlayerMover,
                PlayerStateMachine.RigidBody,
                PlayerStateMachine.InputReader.MovementValue,
                PlayerStateMachine.PlayerProperties.WalkSpeed);
            CommandInvoker.ExecuteCommand(runCommand);

            ICommand facingCommand = new PlayerFacingCommand(
                PlayerStateMachine.ParentObject,
                PlayerStateMachine.InputReader.MovementValue.x);
            CommandInvoker.ExecuteCommand(facingCommand);
        }
        
        public override void OnExit()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent -= CheckAttackBasic;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart -= CheckOnHurt;
        }

        private void CheckStopMoving()
        {
            if (PlayerStateMachine.InputReader.MovementValue == Vector2.zero)
                PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerIdleState);
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
        
        private void CheckOnHurt(int damage, Vector3 hitPosition, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerHurtState);
        }
    }
}