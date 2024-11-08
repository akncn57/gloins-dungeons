using CustomInterfaces;
using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using UnityEngine.AI;
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
            if (!SkeletonStateMachine.HasLineOfSight)
            {
                ICommand stopMoveCommand = new EnemyStopMoveCommand(
                    SkeletonStateMachine.EnemyMover, 
                    SkeletonStateMachine.Rigidbody);
                CommandInvoker.ExecuteCommand(stopMoveCommand);
                
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
                return;
            }
            
            ApproachPlayer(_playerGameObject.transform.position);
        }

        public override void OnExit(){}

        private void ApproachPlayer(Vector3 playerPosition)
        {
            switch ((SkeletonStateMachine.Rigidbody.transform.position - playerPosition).magnitude)
            {
                case > 5f:
                {
                    ICommand stopMoveCommand = new EnemyStopMoveCommand(
                        SkeletonStateMachine.EnemyMover, 
                        SkeletonStateMachine.Rigidbody);
                    CommandInvoker.ExecuteCommand(stopMoveCommand);
                
                    SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
                    return;
                }
                case < 0.1f:
                    SkeletonStateMachine.ParentObject.transform.localScale = _playerGameObject.transform.position.x < SkeletonStateMachine.Rigidbody.position.x 
                        ? new Vector3(-1f, 1f, 1f) 
                        : new Vector3(1f, 1f, 1f);
                
                    SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonAttackBasicState);
                    return;
            }

            ICommand setDestinationCommand = new EnemySetDestinationCommand(
                SkeletonStateMachine.EnemySetDestination,
                SkeletonStateMachine.GetComponent<NavMeshAgent>(),
                _playerGameObject.transform.position);
            CommandInvoker.ExecuteCommand(setDestinationCommand);

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