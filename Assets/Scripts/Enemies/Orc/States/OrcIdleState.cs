using Tools;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcIdleState : OrcBaseState
    {
        private GenericTimer _genericTimer;
        
        protected OrcIdleState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        public override void OnEnter()
        {
            OrcStateMachine.Animator.Play("Idle-BlendTree");
            
            _genericTimer = Instantiator.Instantiate<GenericTimer>(new object[]{3f});
            _genericTimer.OnTimerFinished += CheckIdleTimeFinished;
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            _genericTimer.OnTimerFinished -= CheckIdleTimeFinished;
        }
        
        private void CheckIdleTimeFinished()
        {
            OrcStateMachine.SwitchState(OrcStateMachine.OrcPatrolState);
        }
    }
}