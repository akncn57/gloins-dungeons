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
        private static readonly int Horizontal = Animator.StringToHash("Horizontal"); // Hash for Horizontal animation parameter
        private static readonly int Vertical = Animator.StringToHash("Vertical"); // Hash for Vertical animation parameter
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal"); // Hash for LastHorizontal animation parameter
        private static readonly int LastVertical = Animator.StringToHash("LastVertical"); // Hash for LastVertical animation parameter

        /// <summary>
        /// Initializes the patrol state with necessary dependencies.
        /// </summary>
        protected OrcPatrolState(
            OrcStateMachine orcStateMachine, 
            IInstantiator instantiator, 
            SignalBus signalBus, 
            CoroutineRunner coroutineRunner, 
            CameraShake cameraShake) 
            : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        /// <summary>
        /// Called when entering the patrol state.
        /// </summary>
        public override void OnEnter()
        {
            _lastPosition = OrcStateMachine.EnemyNavMeshAgent.transform.position; // Initialize the last position
            OrcStateMachine.Animator.Play("Walk-BlendTree"); // Play the walking animation
            HandlePatrolInitialization(); // Initialize or reset patrol state
        }

        /// <summary>
        /// Called once per frame during the patrol state.
        /// </summary>
        public override void OnTick()
        {
            // Check if a player is within the chase area
            var drawChaseOverlayCommand = new EnemyDrawChaseOverlayCommand(
                OrcStateMachine.EnemyDrawChaseOverlay,
                OrcStateMachine.Rigidbody.position, 
                OrcStateMachine.EnemyProperties.ChaseRadius, 
                OrcStateMachine);
            
            var isPlayerWithinChaseArea = (bool)CommandInvoker.ExecuteCommand(drawChaseOverlayCommand);

            if (isPlayerWithinChaseArea && OrcStateMachine.HasLineOfSight)
            {
                // Stop the NavMeshAgent to halt navigation-based movement
                var stopAgentCommand = new EnemyStopMovementCommand(
                    OrcStateMachine.EnemyStopMovement, 
                    OrcStateMachine.EnemyNavMeshAgent);
                CommandInvoker.ExecuteCommand(stopAgentCommand);
                
                // Switch to chase state if a player is detected
                OrcStateMachine.SwitchState(OrcStateMachine.OrcChaseState);
                return;
            }

            // Continue patrolling if the current coordinate is not completed
            if (!OrcStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted)
            {
                MoveToPatrolCoordinate(OrcStateMachine.PatrolCoordinates[_patrolIndex].PatrolCoordinate.position);
            }

            // Update animation parameters for movement direction
            UpdateMovementAnimations();

            _lastPosition = OrcStateMachine.EnemyNavMeshAgent.transform.position; // Update the last position
        }

        /// <summary>
        /// Called when exiting the patrol state.
        /// </summary>
        public override void OnExit()
        {
            UpdatePatrolIndex();
        }

        /// <summary>
        /// Initializes the patrol logic by setting or resetting patrol coordinates and indices.
        /// </summary>
        private void HandlePatrolInitialization()
        {
            if (OrcStateMachine.PatrolCoordinates == null || OrcStateMachine.PatrolCoordinates.Count == 0)
            {
                Debug.LogError("Patrol coordinates are not set.");
                return;
            }

            if (_patrolIndex >= OrcStateMachine.PatrolCoordinates.Count - 1)
            {
                OrcStateMachine.ResetPatrolCoordinateStatus(); // Reset all patrol statuses
                OrcStateMachine.PatrolCoordinates.Reverse(); // Reverse the patrol route
                _patrolIndex = 0; // Reset patrol index to start of reversed path
            }
        }

        /// <summary>
        /// Updates the patrol index to ensure the orc continues to the next patrol point.
        /// </summary>
        private void UpdatePatrolIndex()
        {
            _patrolIndex = (_patrolIndex + 1) % OrcStateMachine.PatrolCoordinates.Count;
        }

        /// <summary>
        /// Moves the orc to the specified patrol coordinate.
        /// If the coordinate is reached, marks it as complete.
        /// </summary>
        /// <param name="coordinate">Target patrol coordinate.</param>
        private void MoveToPatrolCoordinate(Vector3 coordinate)
        {
            if (HasReachedCoordinate(coordinate))
            {
                CompletePatrolCoordinate();
                return;
            }

            SetPatrolDestination(coordinate);
        }

        /// <summary>
        /// Determines if the orc has reached the given coordinate.
        /// </summary>
        /// <param name="coordinate">Target coordinate to check.</param>
        /// <returns>True if the orc is within a small threshold of the target coordinate, otherwise false.</returns>
        private bool HasReachedCoordinate(Vector3 coordinate)
        {
            return (OrcStateMachine.Rigidbody.transform.position - coordinate).magnitude < 0.6f;
        }

        /// <summary>
        /// Marks the current patrol coordinate as completed and transitions to idle state.
        /// </summary>
        private void CompletePatrolCoordinate()
        {
            OrcStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted = true;

            ExecuteCommand(new EnemyStopMovementCommand(
                OrcStateMachine.EnemyStopMovement, 
                OrcStateMachine.EnemyNavMeshAgent));

            OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState); // Switch to idle state
        }

        /// <summary>
        /// Sets the patrol destination for the orc's NavMeshAgent.
        /// </summary>
        /// <param name="coordinate">Target destination coordinate.</param>
        private void SetPatrolDestination(Vector3 coordinate)
        {
            OrcStateMachine.EnemyNavMeshAgent.speed = OrcStateMachine.EnemyProperties.WalkSpeed;
            
            ExecuteCommand(new EnemySetDestinationCommand(
                OrcStateMachine.EnemySetDestination,
                OrcStateMachine.EnemyNavMeshAgent,
                coordinate));
        }

        /// <summary>
        /// Executes a given command using the command pattern.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        private void ExecuteCommand(ICommand command)
        {
            CommandInvoker.ExecuteCommand(command);
        }

        /// <summary>
        /// Updates animation parameters based on the orc's movement direction.
        /// </summary>
        private void UpdateMovementAnimations()
        {
            var agentVelocity = OrcStateMachine.EnemyNavMeshAgent.velocity.normalized;
            float horizontal = Mathf.RoundToInt(agentVelocity.x);
            float vertical = Mathf.RoundToInt(agentVelocity.y);

            OrcStateMachine.Animator.SetFloat(Horizontal, horizontal);
            OrcStateMachine.Animator.SetFloat(Vertical, vertical);

            if (OrcStateMachine.EnemyNavMeshAgent.velocity != Vector3.zero)
            {
                OrcStateMachine.Animator.SetFloat(LastHorizontal, horizontal);
                OrcStateMachine.Animator.SetFloat(LastVertical, vertical);
            }
        }
    }
}