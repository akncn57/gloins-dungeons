using CustomInterfaces;
using DesignPatterns.CommandPattern;
using Enemies.Commands;
using EventInterfaces;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonChaseState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        private GameObject _playerGameObject;
        private ICommand _findClosestChasePositionCommand;

        public SkeletonChaseState(
            SkeletonStateMachine skeletonStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(skeletonStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}

        public override void OnEnter()
        {
            SignalBus.Subscribe<IPlayerEvents.OnPlayerAttacked>(CheckPlayerAttack);
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            
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
            SignalBus.Unsubscribe<IPlayerEvents.OnPlayerAttacked>(CheckPlayerAttack);
            SkeletonStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
            
            SkeletonStateMachine.ExclamationMarkObject.SetActive(false);
        }

        private void ApproachPlayer(Vector3 playerPosition)
        {
            SkeletonStateMachine.ExclamationMarkObject.SetActive(true);
            
            // ICommand findClosetPositionCommand = new EnemyFindClosestChasePointCommand(
            //     SkeletonStateMachine.EnemyFindClosestChasePoint, 
            //     SkeletonStateMachine.transform.position, 
            //     _playerGameObject.transform.position,
            //     SkeletonStateMachine.EnemyProperties.ChasePositionOffset);

            // var newPosition = CommandInvoker.ExecuteCommand(findClosetPositionCommand);
            var enemyPositionToChasePoint = (SkeletonStateMachine.Rigidbody.transform.position - _playerGameObject.transform.position).magnitude;
                
            switch ((SkeletonStateMachine.Rigidbody.transform.position - playerPosition).magnitude)
            {
                case > 5f:
                {
                    SkeletonStateMachine.IsEnemyNearToPlayer = false;
                        
                    ICommand stopMoveCommand = new EnemyStopMovementCommand(
                        SkeletonStateMachine.EnemyStopMovement, 
                        SkeletonStateMachine.EnemyNavMeshAgent);
                    CommandInvoker.ExecuteCommand(stopMoveCommand);
                
                    SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
                    return;
                }
                case <= 2f:
                    SkeletonStateMachine.IsEnemyNearToPlayer = true;
                    
                    if (enemyPositionToChasePoint <= 1f && !SkeletonStateMachine.IsBlocking)
                    {
                        var randomHeavyAttackChange = Random.Range(0f, 1f);
                        
                        if (randomHeavyAttackChange <= SkeletonStateMachine.EnemyProperties.HeavyAttackChance)
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
                    return;
            }

            ICommand setDestinationCommand = new EnemySetDestinationCommand(
                SkeletonStateMachine.EnemySetDestination,
                SkeletonStateMachine.EnemyNavMeshAgent,
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

        private void CheckPlayerAttack()
        {
            if (!SkeletonStateMachine.IsEnemyNearToPlayer) return;
            
            var randomBlockChange = Random.Range(0f, 1f);
            
            if (randomBlockChange <= SkeletonStateMachine.EnemyProperties.BlockChance)
            {
                SkeletonStateMachine.EnemyNavMeshAgent.isStopped = true;
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonBlockState);
            }
        }
        
        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            if (!SkeletonStateMachine.IsBlocking)
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonHurtState);
        }
    }
}