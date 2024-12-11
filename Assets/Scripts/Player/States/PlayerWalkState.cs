using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Player.States
{
    public class PlayerWalkState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Walk;
        
        // private readonly int _walkAnimationHash = Animator.StringToHash("Warrior_Walk");
        
        public PlayerWalkState(
            PlayerStateMachine playerStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(playerStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}

        public override void OnEnter()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent += CheckAttackBasic;
            PlayerStateMachine.InputReader.AttackHeavyEvent += CheckAttackHeavy;
            PlayerStateMachine.InputReader.DashEvent += CheckDash;
            PlayerStateMachine.PlayerColliderController.OnHitStart += CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart += CheckOnHurt;
            
            // PlayerStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
            
            PlayerStateMachine.Animator.Play("Walk-BlendTree");
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

            // ICommand facingCommand = new PlayerFacingCommand(
            //     PlayerStateMachine.PlayerFacing,
            //     PlayerStateMachine.ParentObject,
            //     PlayerStateMachine.InputReader.MovementValue.x);
            // CommandInvoker.ExecuteCommand(facingCommand);
            
            PlayerStateMachine.Animator.SetFloat("Horizontal", PlayerStateMachine.InputReader.MovementValue.x);
            PlayerStateMachine.Animator.SetFloat("Vertical", PlayerStateMachine.InputReader.MovementValue.y);

            if (PlayerStateMachine.InputReader.MovementValue != Vector2.zero)
            {
                PlayerStateMachine.Animator.SetFloat("LastHorizontal", PlayerStateMachine.InputReader.MovementValue.x);
                PlayerStateMachine.Animator.SetFloat("LastVertical", PlayerStateMachine.InputReader.MovementValue.y);
            }
        }
        
        public override void OnExit()
        {
            PlayerStateMachine.InputReader.AttackBasicEvent -= CheckAttackBasic;
            PlayerStateMachine.InputReader.AttackHeavyEvent -= CheckAttackHeavy;
            PlayerStateMachine.InputReader.DashEvent -= CheckDash;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart -= CheckOnHurt;
            
            PlayerStateMachine.Animator.SetBool("IsWalking" , false);
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