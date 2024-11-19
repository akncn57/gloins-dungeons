using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonDeathState : SkeletonBaseState
    {
        private readonly int _deathAnimationHash = Animator.StringToHash("Skeleton_Death");
        
        public SkeletonDeathState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator, SignalBus signalBus) : base(skeletonStateMachine, instantiator, signalBus){}

        public override void OnEnter()
        {
            Debug.Log("Enemy Skeleton Death!");
            SkeletonStateMachine.Collider.enabled = false;
            
            ICommand stopMoveCommand = new EnemyStopMovementCommand(
                SkeletonStateMachine.EnemyStopMovement, 
                SkeletonStateMachine.EnemyNavMeshAgent);
            CommandInvoker.ExecuteCommand(stopMoveCommand);

            ICommand stopRigidbodyCommand = new EnemyStopRigidbodyCommand(
                SkeletonStateMachine.EnemyStopRigidbody,
                SkeletonStateMachine.Rigidbody);
            CommandInvoker.ExecuteCommand(stopRigidbodyCommand);
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_deathAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}