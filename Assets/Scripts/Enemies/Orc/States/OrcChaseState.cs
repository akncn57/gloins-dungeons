using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    /// <summary>
    /// Represents the Orc's chasing state, where the Orc pursues the player.
    /// </summary>
    public class OrcChaseState : OrcBaseState
    {
        private Vector3 _lastPosition; // Stores the last recorded position for movement calculation
        private static readonly int Horizontal = Animator.StringToHash("Horizontal"); // Hash for Horizontal animation parameter
        private static readonly int Vertical = Animator.StringToHash("Vertical"); // Hash for Vertical animation parameter
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal"); // Hash for LastHorizontal animation parameter
        private static readonly int LastVertical = Animator.StringToHash("LastVertical"); // Hash for LastVertical animation parameter

        /// <summary>
        /// Initializes the chase state with dependencies.
        /// </summary>
        protected OrcChaseState(
            OrcStateMachine orcStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake)
            : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        /// <summary>
        /// Called when entering the chase state. Plays the running animation.
        /// </summary>
        public override void OnEnter()
        {
            OrcStateMachine.Animator.Play("Run-BlendTree"); // Play the running animation
        }

        /// <summary>
        /// Called on each frame while in the chase state.
        /// </summary>
        public override void OnTick()
        {
            HandleMovementTowardsPlayer();
            HandleStateTransitions();
        }

        /// <summary>
        /// Called when exiting the chase state.
        /// </summary>
        public override void OnExit()
        {
            // No additional actions on exit for now
        }

        /// <summary>
        /// Handles movement towards the player's position.
        /// </summary>
        private void HandleMovementTowardsPlayer()
        {
            if (!OrcStateMachine.HasLineOfSight) return;

            // Set the Orc's movement speed to running speed
            OrcStateMachine.EnemyNavMeshAgent.speed = OrcStateMachine.EnemyProperties.RunSpeed;

            // Update the destination to the player's position
            ExecuteSetDestinationCommand(OrcStateMachine.PlayerCollider.transform.position);

            // Update movement-related animation parameters
            UpdateAnimationParameters(OrcStateMachine.EnemyNavMeshAgent.velocity.normalized);
        }

        /// <summary>
        /// Updates animation parameters based on movement direction.
        /// </summary>
        /// <param name="movementDirection">The normalized direction of movement.</param>
        private void UpdateAnimationParameters(Vector3 movementDirection)
        {
            OrcStateMachine.Animator.SetFloat(Horizontal, movementDirection.x);
            OrcStateMachine.Animator.SetFloat(Vertical, movementDirection.y);

            if (OrcStateMachine.EnemyNavMeshAgent.speed > 0f)
            {
                OrcStateMachine.Animator.SetFloat(LastHorizontal, movementDirection.x);
                OrcStateMachine.Animator.SetFloat(LastVertical, movementDirection.y);
            }
        }

        /// <summary>
        /// Executes a command to set the Orc's destination.
        /// </summary>
        /// <param name="destination">The target position for the Orc to move towards.</param>
        private void ExecuteSetDestinationCommand(Vector3 destination)
        {
            var setDestinationCommand = new EnemySetDestinationCommand(
                OrcStateMachine.EnemySetDestination,
                OrcStateMachine.EnemyNavMeshAgent,
                destination);

            CommandInvoker.ExecuteCommand(setDestinationCommand);
        }

        /// <summary>
        /// Handles state transitions based on the Orc's distance to the player.
        /// </summary>
        private void HandleStateTransitions()
        {
            var distanceToPlayer = (OrcStateMachine.Rigidbody.transform.position -
                                    OrcStateMachine.PlayerCollider.transform.position).magnitude;

            if (distanceToPlayer <= 1f)
            {
                // If close to the player, prepare to attack
                Debug.Log("Orc ready to Attack!");
            }
            else if (distanceToPlayer >= 4f)
            {
                // If the player is too far, switch to idle state
                OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
            }
        }
    }
}
