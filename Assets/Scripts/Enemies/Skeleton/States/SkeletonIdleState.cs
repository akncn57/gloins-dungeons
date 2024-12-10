using CustomInterfaces;
using DesignPatterns.CommandPattern;
using Enemies.Skeleton.Commands;
using Tools;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Skeleton.States
{
    public class SkeletonIdleState : SkeletonBaseState
    {
        private readonly int _idleAnimationHash = Animator.StringToHash("Skeleton_Idle");
        private GenericTimer _genericTimer;
        
        public SkeletonIdleState(
            SkeletonStateMachine skeletonStateMachine,
            IInstantiator instantiator,
            SignalBus signalBus,
            CoroutineRunner coroutineRunner,
            CameraShake cameraShake) : base(skeletonStateMachine, instantiator, signalBus, coroutineRunner, cameraShake){}
        
        public override void OnEnter()
        {
            _genericTimer = Instantiator.Instantiate<GenericTimer>(new object[]{3f});
            
            SkeletonStateMachine.EnemyColliderController.OnHitStart += CheckOnHurt;
            _genericTimer.OnTimerFinished += CheckIdleWaitFinished;
            
            SkeletonStateMachine.Animator.CrossFadeInFixedTime(_idleAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            ICommand drawChaseOverlayCommand = new SkeletonDrawChaseOverlayCommand(
                SkeletonStateMachine.SkeletonDrawChaseOverlay, 
                SkeletonStateMachine.transform.position,
                SkeletonStateMachine.EnemyProperties.ChaseRadius,
                SkeletonStateMachine);
            CommandInvoker.ExecuteCommand(drawChaseOverlayCommand);
        }

        public override void OnExit()
        {
            SkeletonStateMachine.EnemyColliderController.OnHitStart -= CheckOnHurt;
            _genericTimer.OnTimerFinished -= CheckIdleWaitFinished;
        }

        private void CheckOnHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            if (!SkeletonStateMachine.IsBlocking)
                SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonHurtState);
        }

        private void CheckIdleWaitFinished()
        {
            SkeletonStateMachine.SwitchState(SkeletonStateMachine.SkeletonPatrolState);
        }
    }
}