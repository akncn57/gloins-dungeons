using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcChaseState : OrcBaseState
    {
        private Vector3 _lastPosition; // Stores the last recorded position for movement calculation
        private static readonly int Horizontal = Animator.StringToHash("Horizontal"); // Hash for Horizontal animation parameter
        private static readonly int Vertical = Animator.StringToHash("Vertical"); // Hash for Vertical animation parameter
        private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal"); // Hash for LastHorizontal animation parameter
        private static readonly int LastVertical = Animator.StringToHash("LastVertical"); // Hash for LastVertical animation parameter
        
        protected OrcChaseState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        /// <summary>
        /// Called when entering the chase state.
        /// </summary>
        public override void OnEnter()
        {
            OrcStateMachine.Animator.Play("Run-BlendTree"); // Play the walking animation
        }

        /// <summary>
        /// Called once per frame during the chase state.
        /// </summary>
        public override void OnTick()
        {
            if (OrcStateMachine.HasLineOfSight)
            {
                OrcStateMachine.EnemyNavMeshAgent.speed = OrcStateMachine.EnemyProperties.RunSpeed;
                
                var enemySetDestinationCommand = new EnemySetDestinationCommand(
                    OrcStateMachine.EnemySetDestination,
                    OrcStateMachine.EnemyNavMeshAgent,
                    OrcStateMachine.PlayerCollider.gameObject.transform.position);
                CommandInvoker.ExecuteCommand(enemySetDestinationCommand);
                
                var movement = OrcStateMachine.EnemyNavMeshAgent.transform.position - _lastPosition;
                var movementDirection = new Vector2(movement.x, movement.y).normalized;

                OrcStateMachine.Animator.SetFloat(Horizontal, movementDirection.x);
                OrcStateMachine.Animator.SetFloat(Vertical, movementDirection.y);

                if (OrcStateMachine.EnemyNavMeshAgent.speed != 0f)
                {
                    OrcStateMachine.Animator.SetFloat(LastHorizontal, movementDirection.x);
                    OrcStateMachine.Animator.SetFloat(LastVertical, movementDirection.y);
                }
            }

            var orcPositionToPlayer = (OrcStateMachine.Rigidbody.transform.position -
                                       OrcStateMachine.PlayerCollider.transform.position).magnitude;
            
            switch (orcPositionToPlayer)
            {
                case <= 1f:
                    Debug.Log("Orc ready to Attack!");
                    break;
                case >= 4f:
                    OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
                    break;
            }
        }

        /// <summary>
        /// Called when exiting the chase state.
        /// </summary>
        public override void OnExit()
        {
            
        }
    }
}