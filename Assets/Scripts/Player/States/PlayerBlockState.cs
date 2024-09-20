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
            PlayerStateMachine.PlayerColliderController.OnHitStart += CheckOnHurt;
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
            PlayerStateMachine.PlayerColliderController.OnHitStart -= CheckOnHurt;
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
            IsAttackHitFrontOfPlayer(hitPosition);
        }

        private void IsAttackHitFrontOfPlayer(Vector3 hitDirection)
        {
            var transform = PlayerStateMachine.RigidBody.transform;
            var directionToTarget = (hitDirection - transform.position).normalized;
            var forward = PlayerStateMachine.ParentObject.transform.localScale.x == 1f ? transform.right : transform.right * -1;
            var dotProduct = Vector2.Dot(forward, directionToTarget);
            
            Debug.Log("\n transform : " + transform + 
                      "\n directionToTarget : " + directionToTarget + 
                      "\n forward : " + forward + 
                      "\n dotProduct : " + dotProduct);
            
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