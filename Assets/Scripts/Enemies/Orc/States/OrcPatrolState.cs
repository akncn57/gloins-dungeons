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
        private Vector3 _lastPosition; // Stores the last recorded position for movement calculation

        protected OrcPatrolState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) 
            : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        public override void OnEnter()
        {
            _lastPosition = OrcStateMachine.EnemyNavMeshAgent.transform.position; // Initialize the last position
            
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
            
            // Calculate movement direction
            var movement = OrcStateMachine.EnemyNavMeshAgent.transform.position - _lastPosition;
            var movementDirection = new Vector2(movement.x, movement.y).normalized;

            // Update animation parameters for movement direction
            OrcStateMachine.Animator.SetFloat("Horizontal", movementDirection.x);
            OrcStateMachine.Animator.SetFloat("Vertical", movementDirection.y);

            // Store the last movement direction if the orc is moving
            if (OrcStateMachine.EnemyNavMeshAgent.speed != 0f)
            {
                OrcStateMachine.Animator.SetFloat("LastHorizontal", movementDirection.x);
                OrcStateMachine.Animator.SetFloat("LastVertical", movementDirection.y);
            }
            
            _lastPosition = OrcStateMachine.EnemyNavMeshAgent.transform.position; // Update the last position
        }

        public override void OnExit()
        {
            // Update the patrol index when exiting the state
            UpdatePatrolIndex();
        }

        private void HandlePatrolInitialization()
        {
            // Ensure patrol coordinates are set
            if (OrcStateMachine.PatrolCoordinates == null || OrcStateMachine.PatrolCoordinates.Count == 0)
            {
                Debug.LogError("Patrol coordinates are not set.");
                return;
            }

            // Check if the patrol index exceeds the list bounds
            if (_patrolIndex >= OrcStateMachine.PatrolCoordinates.Count - 1)
            {
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

            // Set a new destination for the orc
            SetPatrolDestination(coordinate);
        }

        private bool HasReachedCoordinate(Vector3 coordinate)
        {
            // Determine if the orc is within a small threshold of the target coordinate
            return (OrcStateMachine.Rigidbody.transform.position - coordinate).magnitude < 0.6f;
        }

        private void CompletePatrolCoordinate()
        {
            // Mark the current patrol coordinate as completed
            OrcStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted = true;

            // Stop the orc's movement
            ExecuteCommand(new EnemyStopMovementCommand(
                OrcStateMachine.EnemyStopMovement, 
                OrcStateMachine.EnemyNavMeshAgent));

            // Switch to idle state
            OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
        }

        private void SetPatrolDestination(Vector3 coordinate)
        {
            // Set a new navigation destination for the orc
            ExecuteCommand(new EnemySetDestinationCommand(
                OrcStateMachine.EnemySetDestination,
                OrcStateMachine.EnemyNavMeshAgent,
                coordinate));
        }

        private void ExecuteCommand(ICommand command)
        {
            // Execute the given command using the command pattern
            CommandInvoker.ExecuteCommand(command);
        }
    }
}