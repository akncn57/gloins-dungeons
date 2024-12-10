using DesignPatterns.CommandPattern;
using Enemies.Commands;
using Tools;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonBlockState : SkeletonBaseState
    {
        private readonly int _blockUpAnimationHash = Animator.StringToHash("Skeleton_BlockUp");
        private readonly int _blockDownAnimationHash = Animator.StringToHash("Skeleton_BlockDown");
        private GenericTimer _genericTimer;
        
        protected SkeletonBlockState(
            SkeletonStateMachine skeletonStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(skeletonStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}

        public override void OnEnter()
        {
            _genericTimer = Instantiator.Instantiate<GenericTimer>(new object[]{SkeletonStateMachine.EnemyProperties.BlockDuration});
            _genericTimer.OnTimerFinished += CheckBlockTimeFinished;
            
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            
            ICommand stopMoveCommand = new EnemyStopMovementCommand(
                SkeletonStateMachine.EnemyStopMovement, 
                SkeletonStateMachine.EnemyNavMeshAgent);
            CommandInvoker.ExecuteCommand(stopMoveCommand);
            
            ICommand stopRigidbodyCommand = new EnemyStopRigidbodyCommand(
                SkeletonStateMachine.EnemyStopRigidbody, 
                SkeletonStateMachine.Rigidbody);
            CommandInvoker.ExecuteCommand(stopRigidbodyCommand);

            SkeletonStateMachine.IsBlocking = true;
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_blockUpAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            _genericTimer.OnTimerFinished -= CheckBlockTimeFinished;
            
            SkeletonStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_blockDownAnimationHash, 0.1f);
        }
        
        private void CheckBlockTimeFinished()
        {
            SkeletonStateMachine.IsBlocking = false;
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonIdleState);
        }
        
        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            SkeletonStateMachine.BlockEffectParticle.Play();
        }
    }
}