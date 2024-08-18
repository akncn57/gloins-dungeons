using CustomInterfaces;
using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonChaseState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        private GameObject _playerGameObject;

        public SkeletonChaseState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

        public override void OnEnter()
        {
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            //TODO: Return biciminde komut calistirmak lazim.
            // ICommand findClosestChasePositionCommand = new EnemyFindClosestChasePointCommand(
            //     SkeletonStateMachine.EnemyFindClosestChasePoint,
            //     _playerGameObject.transform.position,
            //     SkeletonStateMachine.Rigidbody.position,
            //     SkeletonStateMachine.ChasePositionOffset);
            // CommandInvoker.ExecuteCommand(findClosestChasePositionCommand);
                
            ApproachPlayer(FindClosestPosition());
        }

        public override void OnExit(){}

        private void ApproachPlayer(Vector3 playerPosition)
        {
            if ((SkeletonStateMachine.Rigidbody.transform.position - playerPosition).magnitude < 0.1f)
            {
                SkeletonStateMachine.ParentObject.transform.localScale = _playerGameObject.transform.position.x < SkeletonStateMachine.Rigidbody.position.x 
                    ? new Vector3(-1f, 1f, 1f) 
                    : new Vector3(1f, 1f, 1f);
                
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonAttackBasicState);
                return;
            }
            
            ICommand moveCommand = new EnemyMoveCommand(
                SkeletonStateMachine.EnemyMover,
                playerPosition, SkeletonStateMachine.Rigidbody,
                SkeletonStateMachine.WalkSpeed);
            CommandInvoker.ExecuteCommand(moveCommand);

            ICommand facingCommand = new EnemyFacingCommand(
                SkeletonStateMachine.EnemyFacing,
                SkeletonStateMachine.ParentObject,
                playerPosition.x - SkeletonStateMachine.Rigidbody.transform.position.x);
            CommandInvoker.ExecuteCommand(facingCommand);
        }
        
        private Vector3 FindClosestPosition()
        {
            var playerPosition = _playerGameObject.transform.position;
            return SkeletonStateMachine.Rigidbody.position.x <= playerPosition.x 
                ? new Vector3(playerPosition.x - SkeletonStateMachine.ChasePositionOffset, playerPosition.y, 0f) 
                : new Vector3(playerPosition.x + SkeletonStateMachine.ChasePositionOffset, playerPosition.y, 0f);
        }

        public void Init(IPlayer player)
        {
            _playerGameObject = player.GameObject;
        }
    }
}