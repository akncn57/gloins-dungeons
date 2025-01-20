using DesignPatterns.CommandPattern;
using Enemies.Commands;
using Enemies.Orc;
using UtilScripts;
using Zenject;
using UnityEngine;

namespace Enemies.Golem.States
{
    public class GolemPatrolState : GolemBaseState
    {
        private int _patrolIndex; // Tracks the current patrol coordinate index
        private Vector3 _lastPosition; // Stores the last recorded position for movement calculation
        private static readonly int Horizontal = Animator.StringToHash("Horizontal"); // Hash for Horizontal animation parameter
        private static readonly int Vertical = Animator.StringToHash("Vertical"); // Hash for Vertical animation parameter
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal"); // Hash for LastHorizontal animation parameter
        private static readonly int LastVertical = Animator.StringToHash("LastVertical"); // Hash for LastVertical animation parameter
        
        protected GolemPatrolState(GolemStateMachine golemStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(golemStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }
        
        /// <summary>
        /// Called when entering the patrol state.
        /// </summary>
        public override void OnEnter()
        {
            GolemStateMachine.EnemyColliderController.OnHitStart += CheckHurt;
            
            _lastPosition = GolemStateMachine.EnemyNavMeshAgent.transform.position; // Initialize the last position
            GolemStateMachine.Animator.Play("Walk-BlendTree"); // Play the walking animation
            HandlePatrolInitialization(); // Initialize or reset patrol state
        }

        /// <summary>
        /// Called once per frame during the patrol state.
        /// </summary>
        public override void OnTick()
        {
            // Check if a player is within the chase area
            var drawChaseOverlayCommand = new EnemyDrawChaseOverlayCommand(
                GolemStateMachine.EnemyDrawChaseOverlay,
                GolemStateMachine.Rigidbody.position, 
                GolemStateMachine.EnemyProperties.ChaseRadius, 
                GolemStateMachine);
            
            var isPlayerWithinChaseArea = (bool)CommandInvoker.ExecuteCommand(drawChaseOverlayCommand);

            if (isPlayerWithinChaseArea && GolemStateMachine.HasLineOfSight)
            {
                // Stop the NavMeshAgent to halt navigation-based movement
                var stopAgentCommand = new EnemyStopMovementCommand(
                    GolemStateMachine.EnemyStopMovement, 
                    GolemStateMachine.EnemyNavMeshAgent);
                CommandInvoker.ExecuteCommand(stopAgentCommand);
                
                // Switch to chase state if a player is detected
                GolemStateMachine.SwitchState(GolemStateMachine.GolemChaseState);
                return;
            }

            // Continue patrolling if the current coordinate is not completed
            if (!GolemStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted)
            {
                MoveToPatrolCoordinate(GolemStateMachine.PatrolCoordinates[_patrolIndex].PatrolCoordinate.position);
            }

            // Update animation parameters for movement direction
            UpdateMovementAnimations();

            _lastPosition = GolemStateMachine.EnemyNavMeshAgent.transform.position; // Update the last position
        }

        /// <summary>
        /// Called when exiting the patrol state.
        /// </summary>
        public override void OnExit()
        {
            GolemStateMachine.EnemyColliderController.OnHitStart -= CheckHurt;
            
            UpdatePatrolIndex();
        }

        /// <summary>
        /// Initializes the patrol logic by setting or resetting patrol coordinates and indices.
        /// </summary>
        private void HandlePatrolInitialization()
        {
            if (GolemStateMachine.PatrolCoordinates == null || GolemStateMachine.PatrolCoordinates.Count == 0)
            {
                Debug.LogError("Patrol coordinates are not set.");
                return;
            }

            if (_patrolIndex >= GolemStateMachine.PatrolCoordinates.Count - 1)
            {
                GolemStateMachine.ResetPatrolCoordinateStatus(); // Reset all patrol statuses
                GolemStateMachine.PatrolCoordinates.Reverse(); // Reverse the patrol route
                _patrolIndex = 0; // Reset patrol index to start of reversed path
            }
        }

        /// <summary>
        /// Updates the patrol index to ensure the orc continues to the next patrol point.
        /// </summary>
        private void UpdatePatrolIndex()
        {
            _patrolIndex = (_patrolIndex + 1) % GolemStateMachine.PatrolCoordinates.Count;
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
            return (GolemStateMachine.Rigidbody.transform.position - coordinate).magnitude < 0.6f;
        }

        /// <summary>
        /// Marks the current patrol coordinate as completed and transitions to idle state.
        /// </summary>
        private void CompletePatrolCoordinate()
        {
            GolemStateMachine.PatrolCoordinates[_patrolIndex].IsCompleted = true;

            ExecuteCommand(new EnemyStopMovementCommand(
                GolemStateMachine.EnemyStopMovement, 
                GolemStateMachine.EnemyNavMeshAgent));

            GolemStateMachine.SwitchState(GolemStateMachine.GolemIdleState); // Switch to idle state
        }

        /// <summary>
        /// Sets the patrol destination for the orc's NavMeshAgent.
        /// </summary>
        /// <param name="coordinate">Target destination coordinate.</param>
        private void SetPatrolDestination(Vector3 coordinate)
        {
            GolemStateMachine.EnemyNavMeshAgent.speed = GolemStateMachine.EnemyProperties.WalkSpeed;
            
            ExecuteCommand(new EnemySetDestinationCommand(
                GolemStateMachine.EnemySetDestination,
                GolemStateMachine.EnemyNavMeshAgent,
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
            var agentVelocity = GolemStateMachine.EnemyNavMeshAgent.velocity.normalized;
            float horizontal = Mathf.RoundToInt(agentVelocity.x);
            float vertical = Mathf.RoundToInt(agentVelocity.y);

            GolemStateMachine.Animator.SetFloat(Horizontal, horizontal);
            GolemStateMachine.Animator.SetFloat(Vertical, vertical);

            if (GolemStateMachine.EnemyNavMeshAgent.velocity != Vector3.zero)
            {
                GolemStateMachine.Animator.SetFloat(LastHorizontal, horizontal);
                GolemStateMachine.Animator.SetFloat(LastVertical, vertical);
            }
        }
        
        private void CheckHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            GolemStateMachine.SwitchState(GolemStateMachine.GolemHurtState);
        }
    }
}