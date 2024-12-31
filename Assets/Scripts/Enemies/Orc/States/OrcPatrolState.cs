using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UtilScripts;
using Zenject;
using UnityEngine;

namespace Enemies.Orc.States
{
    public class OrcPatrolState : OrcBaseState
    {
        private int _patrolIndex; // Tracks the current patrol coordinate index
        private Vector3 _lastPosition;

        protected OrcPatrolState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) 
            : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        public override void OnEnter()
        {
            _lastPosition = OrcStateMachine.EnemyNavMeshAgent.transform.position;
            
            // Play the walking animation
            OrcStateMachine.Animator.Play("Walk-BlendTree");

            // Initialize or reset patrol state
            HandlePatrolInitialization();
        }

        public override void OnTick()
        {
            // Proceed to the patrol coordinate if not yet completed
            if (!OrcStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted)
            {
                MoveToPatrolCoordinate(OrcStateMachine.PatrolCoordinates[_patrolIndex].PatrolCoordinate.position);
            }
            
            var movement = OrcStateMachine.EnemyNavMeshAgent.transform.position - _lastPosition;
            var movementDirection = new Vector2(movement.x, movement.z).normalized;
            
            OrcStateMachine.Animator.SetFloat("Horizontal", movementDirection.x);
            OrcStateMachine.Animator.SetFloat("Vertical", movementDirection.y);

            if (OrcStateMachine.EnemyNavMeshAgent.speed != 0f)
            {
                OrcStateMachine.Animator.SetFloat("LastHorizontal", movementDirection.x);
                OrcStateMachine.Animator.SetFloat("LastVertical", movementDirection.y);
            }
        }

        public override void OnExit()
        {
            // Update the patrol index when exiting the state
            UpdatePatrolIndex();
        }

        private void HandlePatrolInitialization()
        {
            if (OrcStateMachine.PatrolCoordinates == null || OrcStateMachine.PatrolCoordinates.Count == 0)
            {
                Debug.LogError("Patrol coordinates are not set.");
                return;
            }

            // Ensure the patrol index is within valid bounds
            if (_patrolIndex >= OrcStateMachine.PatrolCoordinates.Count - 1)
            {
                Debug.Log("Resetting patrol coordinates and reversing path.");

                // Reset all patrol statuses
                OrcStateMachine.ResetPatrolCoordinateStatus();
                
                // Reverse the patrol route
                OrcStateMachine.PatrolCoordinates.Reverse();

                // Reset patrol index to start of reversed path
                _patrolIndex = 0;
            }
        }

        private void UpdatePatrolIndex()
        {
            // Increment patrol index and ensure it wraps around
            _patrolIndex = (_patrolIndex + 1) % OrcStateMachine.PatrolCoordinates.Count;
        }

        private void MoveToPatrolCoordinate(Vector3 coordinate)
        {
            // Check if the orc has reached the target patrol coordinate
            if (HasReachedCoordinate(coordinate))
            {
                CompletePatrolCoordinate();
                return;
            }

            SetPatrolDestination(coordinate);
        }

        private bool HasReachedCoordinate(Vector3 coordinate)
        {
            return (OrcStateMachine.Rigidbody.transform.position - coordinate).magnitude < 0.6f;
        }

        private void CompletePatrolCoordinate()
        {
            OrcStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted = true; // Mark the coordinate as completed

            ExecuteCommand(new EnemyStopMovementCommand(
                OrcStateMachine.EnemyStopMovement, 
                OrcStateMachine.EnemyNavMeshAgent));

            OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState); // Switch to idle state
        }

        private void SetPatrolDestination(Vector3 coordinate)
        {
            ExecuteCommand(new EnemySetDestinationCommand(
                OrcStateMachine.EnemySetDestination,
                OrcStateMachine.EnemyNavMeshAgent,
                coordinate));
        }

        private void ExecuteCommand(ICommand command)
        {
            CommandInvoker.ExecuteCommand(command);
        }
    }
}