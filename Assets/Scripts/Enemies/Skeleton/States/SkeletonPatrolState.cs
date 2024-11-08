using DesignPatterns.CommandPattern;
using Enemies.Commands;
using Enemies.Skeleton.Commands;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonPatrolState : SkeletonBaseState
    {
        private readonly int _walkAnimationHash = Animator.StringToHash("Skeleton_Walk");
        private int _patrolIndex;
        private ICommand _drawChaseOverlayCommand;
        
        public SkeletonPatrolState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}
        
        public override void OnEnter()
        {
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_walkAnimationHash, 0.1f);

            if (_patrolIndex >= SkeletonStateMachine.PatrolCoordinates.Count)
            {
                SkeletonStateMachine.ResetPatrolCoordinateStatus();
                SkeletonStateMachine.PatrolCoordinates.Reverse();
                _patrolIndex = 1;
            }
        }

        public override void OnTick()
        {
            _drawChaseOverlayCommand = new SkeletonDrawChaseOverlayCommand(
                SkeletonStateMachine.SkeletonDrawChaseOverlay, 
                SkeletonStateMachine.transform.position,
                SkeletonStateMachine.EnemyProperties.ChaseRadius,
                SkeletonStateMachine);
            CommandInvoker.ExecuteCommand(_drawChaseOverlayCommand);
            
            if (!SkeletonStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted)
                GoPatrolCoordinate(SkeletonStateMachine.PatrolCoordinates[_patrolIndex].PatrolCoordinate.position);
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
            
            _patrolIndex++;
        }
        
        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            if (!SkeletonStateMachine.IsBlocking)
             SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonHurtState);
        }

        private void GoPatrolCoordinate(Vector3 coordinate)
        {
            if ((SkeletonStateMachine.Rigidbody.transform.position - coordinate).magnitude < 0.6f)
            {
                SkeletonStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted = true;
                
                ICommand stopMoveCommand = new EnemyStopMovementCommand(
                    SkeletonStateMachine.EnemyStopMovement, 
                    SkeletonStateMachine.EnemyNavMeshAgent);
                CommandInvoker.ExecuteCommand(stopMoveCommand);
                
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
                return;
            }
            
            ICommand setDestinationCommand = new EnemySetDestinationCommand(
                SkeletonStateMachine.EnemySetDestination,
                SkeletonStateMachine.EnemyNavMeshAgent,
                coordinate);
            CommandInvoker.ExecuteCommand(setDestinationCommand);

            ICommand facingCommand = new EnemyFacingCommand(
                SkeletonStateMachine.EnemyFacing,
                SkeletonStateMachine.ParentObject,
                coordinate.x - SkeletonStateMachine.Rigidbody.transform.position.x);
            CommandInvoker.ExecuteCommand(facingCommand);
        }
    }
}