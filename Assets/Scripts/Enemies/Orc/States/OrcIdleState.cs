using DesignPatterns.CommandPattern;
using Enemies.Commands;
using Tools;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcIdleState : OrcBaseState
    {
        private GenericTimer _idleTimer; // Timer to manage the duration of the idle state
        
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
            OrcStateMachine.EnemyColliderController.OnHitStart += CheckHurt;
            
            StopMovement();     // Ensure the Orc halts all movement
            PlayIdleAnimation(); // Transition to the idle animation
            StartIdleTimer();    // Begin the idle timer
        }

        /// <summary>
        /// Called once per frame while in the idle state.
        /// </summary>
        public override void OnTick()
        {
            if (IsPlayerInChaseArea(out float distanceToPlayer))
            {
                HandlePlayerDetection(distanceToPlayer);
            }
        }

        /// <summary>
        /// Called when exiting the idle state.
        /// </summary>
        public override void OnExit()
        {
            OrcStateMachine.EnemyColliderController.OnHitStart -= CheckHurt;
            
            UnsubscribeFromTimer(); // Prevent memory leaks by unsubscribing from the timer event
        }

        /// <summary>
        /// Stops all movement of the Orc, including navigation and physics-based movement.
        /// </summary>
        private void StopMovement()
        {
            ExecuteCommand(new EnemyStopMovementCommand(
                OrcStateMachine.EnemyStopMovement, 
                OrcStateMachine.EnemyNavMeshAgent));

            ExecuteCommand(new EnemyStopRigidbodyCommand(
                OrcStateMachine.EnemyStopRigidbody, 
                OrcStateMachine.Rigidbody));
        }

        /// <summary>
        /// Plays the idle animation by triggering the appropriate animator state.
        /// </summary>
        private void PlayIdleAnimation()
        {
            OrcStateMachine.Animator.Play("Idle-BlendTree");
        }

        /// <summary>
        /// Starts a timer to define the duration of the idle state.
        /// </summary>
        private void StartIdleTimer()
        {
            _idleTimer = Instantiator.Instantiate<GenericTimer>(new object[] { 3f });
            _idleTimer.OnTimerFinished += HandleIdleTimerFinished;
        }

        /// <summary>
        /// Unsubscribes from the idle timer's completion event to prevent unwanted callbacks.
        /// </summary>
        private void UnsubscribeFromTimer()
        {
            if (_idleTimer != null)
            {
                _idleTimer.OnTimerFinished -= HandleIdleTimerFinished;
            }
        }

        /// <summary>
        /// Handles the event when the idle timer finishes, transitioning the Orc to the patrol state.
        /// </summary>
        private void HandleIdleTimerFinished()
        {
            OrcStateMachine.SwitchState(OrcStateMachine.OrcPatrolState);
        }

        /// <summary>
        /// Checks if a player is within the Orc's chase area and returns the distance to the player if found.
        /// </summary>
        /// <param name="distanceToPlayer">Output parameter for the distance to the detected player.</param>
        /// <returns>True if a player is detected within the chase area; otherwise, false.</returns>
        private bool IsPlayerInChaseArea(out float distanceToPlayer)
        {
            distanceToPlayer = float.MaxValue;

            var drawChaseOverlayCommand = new EnemyDrawChaseOverlayCommand(
                OrcStateMachine.EnemyDrawChaseOverlay,
                OrcStateMachine.Rigidbody.position, 
                OrcStateMachine.EnemyProperties.ChaseRadius, 
                OrcStateMachine);

            var isPlayerDetected = (bool)CommandInvoker.ExecuteCommand(drawChaseOverlayCommand);

            if (isPlayerDetected && OrcStateMachine.HasLineOfSight)
            {
                distanceToPlayer = (OrcStateMachine.Rigidbody.transform.position - 
                                    OrcStateMachine.PlayerCollider.transform.position).magnitude;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Handles logic for when a player is detected within the chase area.
        /// Transitions to either the attack state or the chase state based on the player's distance.
        /// </summary>
        /// <param name="distanceToPlayer">The distance to the detected player.</param>
        private void HandlePlayerDetection(float distanceToPlayer)
        {
            if (distanceToPlayer <= 1f)
            {
                TransitionToAttackState();
            }
            else if (distanceToPlayer <= OrcStateMachine.EnemyProperties.ChaseRadius)
            {
                TransitionToChaseState();
            }
        }

        /// <summary>
        /// Transitions the Orc to the attack state after stopping movement.
        /// </summary>
        private void TransitionToAttackState()
        {
            ExecuteCommand(new EnemyStopMovementCommand(
                OrcStateMachine.EnemyStopMovement, 
                OrcStateMachine.EnemyNavMeshAgent));

            OrcStateMachine.SwitchState(OrcStateMachine.OrcBasicAttackState);
        }

        /// <summary>
        /// Transitions the Orc to the chase state after stopping movement.
        /// </summary>
        private void TransitionToChaseState()
        {
            ExecuteCommand(new EnemyStopMovementCommand(
                OrcStateMachine.EnemyStopMovement, 
                OrcStateMachine.EnemyNavMeshAgent));

            OrcStateMachine.SwitchState(OrcStateMachine.OrcChaseState);
        }

        /// <summary>
        /// Helper method to execute commands through the command invoker.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        private void ExecuteCommand(ICommand command)
        {
            CommandInvoker.ExecuteCommand(command);
        }

        private void CheckHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
             OrcStateMachine.SwitchState(OrcStateMachine.OrcHurtState);
        }
    }
}
