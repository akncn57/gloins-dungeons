using DesignPatterns.CommandPattern;
using Enemies.Commands;
using UnityEngine;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonDeathState : SkeletonBaseState
    {
        private readonly int _deathAnimationHash = Animator.StringToHash("Skeleton_Death");
        
        public SkeletonDeathState(SkeletonStateMachine skeletonStateMachine, IInstantiator instantiator) : base(skeletonStateMachine, instantiator){}

        public override void OnEnter()
        {
            Debug.Log("Enemy Skeleton Death!");
            SkeletonStateMachine.Collider.enabled = false;
            
            ICommand stopMoveCommand = new EnemyStopMoveCommand(SkeletonStateMachine.EnemyMover, SkeletonStateMachine.Rigidbody);
            CommandInvoker.ExecuteCommand(stopMoveCommand);
            
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