using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;

namespace Player.States
{
    public class PlayerAttackBasicState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.AttackBasic;
        
        private readonly int _attackBasicAnimationHash = Animator.StringToHash("Warrior_Attack_Basic");
        private ICommand _attackCommand;
        
        public PlayerAttackBasicState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicOverlapOpen += PlayerOnAttackBasicOpenOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicOverlapClose += PlayerOnAttackBasicCloseOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicFinished += PlayerOnAttackBasicFinish;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart += CheckOnHurt;

            ICommand stopCommand = new PlayerStopMoveCommand(PlayerStateMachine.PlayerMover, PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopCommand);
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_attackBasicAnimationHash, 0.1f);
        }

        public override void OnTick(){}

        public override void OnExit()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicOverlapOpen -= PlayerOnAttackBasicOpenOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicOverlapClose -= PlayerOnAttackBasicCloseOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackBasicFinished -= PlayerOnAttackBasicFinish;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart -= CheckOnHurt;
        }

        private void PlayerOnAttackBasicOpenOverlap()
        {
            _attackCommand = new PlayerAttackBasicCommand(
                PlayerStateMachine.PlayerAttackBasic,
                PlayerStateMachine.AttackBasicCollider,
                PlayerStateMachine.PlayerProperties.BasicAttackPower,
                PlayerStateMachine.PlayerProperties.HitKnockBackPower,
                PlayerStateMachine.RigidBody.position);
            CommandInvoker.ExecuteCommand(_attackCommand);
        }
        
        private void PlayerOnAttackBasicCloseOverlap()
        {
            CommandInvoker.UndoCommand();
        }

        private void PlayerOnAttackBasicFinish()
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerIdleState);
        }
        
        private void CheckOnHurt(int damage, Vector3 hitPosition, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerHurtState);
        }
    }
}