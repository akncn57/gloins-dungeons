using DesignPatterns.CommandPattern;
using EventInterfaces;
using Player.Commands;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Player.States
{
    public class PlayerAttackHeavyState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.AttackBasic;
        
        private readonly int _attackHeavyAnimationHash = Animator.StringToHash("Warrior_Attack_Heavy");
        private ICommand _attackCommand;
        
        public PlayerAttackHeavyState(
            PlayerStateMachine playerStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(playerStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}
        
        public override void OnEnter()
        {
            SignalBus.Fire<IPlayerEvents.OnPlayerAttacked>();
            
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackHeavyOverlapOpen += PlayerOnAttackHeavyOpenOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackHeavyOverlapClose += PlayerOnAttackHeavyCloseOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackHeavyFinished += PlayerOnAttackHeavyFinish;
            PlayerStateMachine.PlayerColliderController.OnHitStart += CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart += CheckOnHurt;
            
            ICommand stopCommand = new PlayerStopMoveCommand(PlayerStateMachine.PlayerMover, PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopCommand);
            
            PlayerStateMachine.Animator.Play("AttackHeavy-BlendTree");
        }

        public override void OnTick() {}

        public override void OnExit()
        {
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackHeavyOverlapOpen -= PlayerOnAttackHeavyOpenOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackHeavyOverlapClose -= PlayerOnAttackHeavyCloseOverlap;
            PlayerStateMachine.PlayerAnimationEventsTrigger.PlayerOnAttackHeavyFinished -= PlayerOnAttackHeavyFinish;
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart -= CheckOnHurt;
        }
        
        private void PlayerOnAttackHeavyOpenOverlap()
        {
            _attackCommand = new PlayerAttackCommand(
                PlayerStateMachine.PlayerAttack,
                PlayerStateMachine.AttackHeavyCollider,
                PlayerStateMachine.PlayerProperties.HeavyAttackPower,
                PlayerStateMachine.PlayerProperties.HeavyAttackHitKnockBackPower,
                PlayerStateMachine.RigidBody.position);
            CommandInvoker.ExecuteCommand(_attackCommand);
            
            CoroutineRunner.StartCoroutine(CameraShake.CameraShakeCor(2f, 0.5f));
        }

        private void PlayerOnAttackHeavyCloseOverlap()
        {
            
        }

        private void PlayerOnAttackHeavyFinish()
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerIdleState);
        }
        
        private void CheckOnHurt(int damage, Vector3 hitPosition, float knockBackStrength)
        {
            PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerHurtState);
        }
    }
}