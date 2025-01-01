using DesignPatterns.CommandPattern;
using Enemies.Commands;
using Tools;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcIdleState : OrcBaseState
    {
        private GenericTimer _idleTimer; // Timer to manage the duration of the idle state
        
        // Constructor to initialize the OrcIdleState with necessary dependencies
        protected OrcIdleState(
            OrcStateMachine orcStateMachine, 
            IInstantiator instantiator, 
            SignalBus signalBus, 
            CoroutineRunner coroutineRunner, 
            CameraShake cameraShake) 
            : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        /// <summary>
        /// Called when entering the idle state.
        /// </summary>
        public override void OnEnter()
        {
            StopMovement(); // Ensure the orc stops all movement
            PlayIdleAnimation(); // Transition to the idle animation
            StartIdleTimer(); // Begin the timer for the idle state
        }

        /// <summary>
        /// Called once per frame during the idle state.
        /// </summary>
        public override void OnTick()
        {
            // No specific logic required during idle state ticks
        }

        /// <summary>
        /// Called when exiting the idle state.
        /// </summary>
        public override void OnExit()
        {
            UnsubscribeFromTimer(); // Unsubscribe from timer event to avoid memory leaks
        }

        /// <summary>
        /// Stops the orc's movement by executing stop commands.
        /// </summary>
        private void StopMovement()
        {
            // Stop the NavMeshAgent to halt navigation-based movement
            var stopAgentCommand = new EnemyStopMovementCommand(
                OrcStateMachine.EnemyStopMovement, 
                OrcStateMachine.EnemyNavMeshAgent);

            // Stop the Rigidbody to halt physics-based movement
            var stopRigidbodyCommand = new EnemyStopRigidbodyCommand(
                OrcStateMachine.EnemyStopRigidbody, 
                OrcStateMachine.Rigidbody);

            // Execute both commands to ensure complete stop
            CommandInvoker.ExecuteCommand(stopAgentCommand);
            CommandInvoker.ExecuteCommand(stopRigidbodyCommand);
        }

        /// <summary>
        /// Plays the idle animation on the orc's animator.
        /// </summary>
        private void PlayIdleAnimation()
        {
            OrcStateMachine.Animator.Play("Idle-BlendTree");
        }

        /// <summary>
        /// Starts the timer for the idle state.
        /// </summary>
        private void StartIdleTimer()
        {
            // Instantiate a timer with a duration of 3 seconds
            _idleTimer = Instantiator.Instantiate<GenericTimer>(new object[] { 3f });

            // Subscribe to the timer's completion event
            _idleTimer.OnTimerFinished += HandleIdleTimerFinished;
        }

        /// <summary>
        /// Unsubscribes from the timer's completion event.
        /// </summary>
        private void UnsubscribeFromTimer()
        {
            if (_idleTimer != null)
            {
                _idleTimer.OnTimerFinished -= HandleIdleTimerFinished;
            }
        }

        /// <summary>
        /// Handles the event when the idle timer finishes.
        /// </summary>
        private void HandleIdleTimerFinished()
        {
            // Switch to the patrol state when idle time ends
            OrcStateMachine.SwitchState(OrcStateMachine.OrcPatrolState);
        }
    }
}