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
        private ICommand _findClosestChasePositionCommand;

        public SkeletonChaseState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

        public override void OnEnter()
        {
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            _findClosestChasePositionCommand = new EnemyFindClosestChasePointCommand(
                 SkeletonStateMachine.EnemyFindClosestChasePoint,
                 SkeletonStateMachine.Rigidbody.position,
                 _playerGameObject.transform.position,
                 SkeletonStateMachine.EnemyProperties.ChasePositionOffset);
                
            ApproachPlayer((Vector3)CommandInvoker.ExecuteCommand(_findClosestChasePositionCommand));
        }

        public override void OnExit(){}

        private void ApproachPlayer(Vector3 playerPosition)
        {
            if ((SkeletonStateMachine.Rigidbody.transform.position - playerPosition).magnitude > 5f)
            {
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
                return;
            }
            
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
                playerPosition,
                SkeletonStateMachine.Rigidbody,
                SkeletonStateMachine.EnemyProperties.WalkSpeed);
            CommandInvoker.ExecuteCommand(moveCommand);

            ICommand facingCommand = new EnemyFacingCommand(
                SkeletonStateMachine.EnemyFacing,
                SkeletonStateMachine.ParentObject,
                playerPosition.x - SkeletonStateMachine.Rigidbody.transform.position.x);
            CommandInvoker.ExecuteCommand(facingCommand);
        }

        public void Init(IPlayer player)
        {
            _playerGameObject = player.GameObject;
        }
    }
}