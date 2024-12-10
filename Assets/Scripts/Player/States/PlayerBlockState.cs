using DesignPatterns.CommandPattern;
using Player.Commands;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Player.States
{
    public class PlayerBlockState : PlayerBaseState
    {
        protected override PlayerStateEnums StateEnum => PlayerStateEnums.Block;
        
        private readonly int _blockAnimationHash = Animator.StringToHash("Warrior_Block_Idle");
        
        public PlayerBlockState(
            PlayerStateMachine playerStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(playerStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}

        public override void OnEnter()
        {
            PlayerStateMachine.PlayerColliderController.OnHitStart += CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart += CheckOnHurt;
            
            PlayerStateMachine.Animator.CrossFadeInFixedTime(_blockAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            ICommand stopMoveCommand = new PlayerStopMoveCommand(PlayerStateMachine.PlayerMover, PlayerStateMachine.RigidBody);
            CommandInvoker.ExecuteCommand(stopMoveCommand);
            
            CheckBlockingFinished();
        }

        public override void OnExit()
        {
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
            PlayerStateMachine.PlayerColliderController.PlayerColliderOnHitStart -= CheckOnHurt;
        }

        private void CheckBlockingFinished()
        {
            if (!PlayerStateMachine.InputReader.IsBlocking)
                PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerIdleState);
        }
        
        private void CheckOnHurt(int damage, Vector3 hitPosition, float knockBackStrength)
        {
            IsAttackHitFrontOfPlayer(hitPosition);
        }

        //TODO: Ayrı bir Command class olabilir mi ?
        private void IsAttackHitFrontOfPlayer(Vector3 hitDirection)
        {
            var transform = PlayerStateMachine.RigidBody.transform;
            var directionToTarget = -hitDirection.normalized;
            var forward = PlayerStateMachine.ParentObject.transform.localScale.x == 1f ? transform.right : transform.right * -1;
            var dotProduct = Vector2.Dot(forward, directionToTarget);
            
            // Debug.Log("\n transform : " + transform + 
            //           "\n directionToTarget : " + directionToTarget + 
            //           "\n forward : " + forward + 
            //           "\n dotProduct : " + dotProduct);
            
            if (dotProduct > 0 && PlayerStateMachine.InputReader.IsBlocking)
            {
                Debug.Log("Player blocked the attack!");

            }
            else
            {
                PlayerStateMachine.SwitchState(PlayerStateMachine.PlayerHurtState);
            }
        }
    }
}