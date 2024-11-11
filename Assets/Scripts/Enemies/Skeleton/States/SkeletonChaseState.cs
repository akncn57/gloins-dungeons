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
                ICommand stopMoveCommand = new EnemyStopMovementCommand(
                    SkeletonStateMachine.EnemyStopMovement, 
                    SkeletonStateMachine.EnemyNavMeshAgent);
                CommandInvoker.ExecuteCommand(stopMoveCommand);
                
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
                return;
            }
            
            ApproachPlayer(_playerGameObject.transform.position);
        }

        public override void OnExit()
        {
            SkeletonStateMachine.ExclamationMarkObject.SetActive(false);
        }

        private void ApproachPlayer(Vector3 playerPosition)
        {
            SkeletonStateMachine.ExclamationMarkObject.SetActive(true);
            
            ICommand findClosetPositionCommand = new EnemyFindClosestChasePointCommand(
                SkeletonStateMachine.EnemyFindClosestChasePoint, 
                SkeletonStateMachine.transform.position, 
                _playerGameObject.transform.position,
                SkeletonStateMachine.EnemyProperties.ChasePositionOffset);

            var newPosition = CommandInvoker.ExecuteCommand(findClosetPositionCommand);
                
            switch ((SkeletonStateMachine.Rigidbody.transform.position - playerPosition).magnitude)
            {
                case > 5f:
                {
                    ICommand stopMoveCommand = new EnemyStopMovementCommand(
                        SkeletonStateMachine.EnemyStopMovement, 
                        SkeletonStateMachine.EnemyNavMeshAgent);
                    CommandInvoker.ExecuteCommand(stopMoveCommand);
                
                    SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
                    return;
                }
                case < 1.5f:
                    if (!SkeletonStateMachine.IsBlocking)
                    {
                        var randomBlockChange = Random.Range(0f, 1f);
                        var randomAttackChange = Random.Range(0f, 1f);
                        
                        if (randomBlockChange <= SkeletonStateMachine.EnemyProperties.BlockChance)
                        {
                            SkeletonStateMachine.EnemyNavMeshAgent.isStopped = true;
                            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonBlockState);
                        }
                        else
                        {
                            if (randomAttackChange <= SkeletonStateMachine.EnemyProperties.HeavyAttackChance)
                            {
                                SkeletonStateMachine.EnemyNavMeshAgent.isStopped = true;
                                SkeletonStateMachine.ParentObject.transform.localScale = _playerGameObject.transform.position.x < SkeletonStateMachine.Rigidbody.position.x 
                                    ? new Vector3(-1f, 1f, 1f) 
                                    : new Vector3(1f, 1f, 1f);
                                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonAttackHeavyState);
                            }
                            else
                            {
                                SkeletonStateMachine.EnemyNavMeshAgent.isStopped = true;
                                SkeletonStateMachine.ParentObject.transform.localScale = _playerGameObject.transform.position.x < SkeletonStateMachine.Rigidbody.position.x 
                                    ? new Vector3(-1f, 1f, 1f) 
                                    : new Vector3(1f, 1f, 1f);
                                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonAttackBasicState);
                            }
                        }
                    }
                    return;
            }

            ICommand setDestinationCommand = new EnemySetDestinationCommand(
                SkeletonStateMachine.EnemySetDestination,
                SkeletonStateMachine.EnemyNavMeshAgent,
                (Vector3)newPosition);
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