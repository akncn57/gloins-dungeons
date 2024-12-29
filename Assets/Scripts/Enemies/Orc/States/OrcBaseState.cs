using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcBaseState : EnemyBaseState
    {
        protected OrcStateMachine OrcStateMachine;
        protected IInstantiator Instantiator;
        protected SignalBus SignalBus;
        protected CoroutineRunner CoroutineRunner;
        protected CameraShake CameraShake;
        
        protected OrcBaseState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake)
        {
            OrcStateMachine = orcStateMachine;
            Instantiator = instantiator;
            SignalBus = signalBus;
            CoroutineRunner = coroutineRunner;
            CameraShake = cameraShake;
        }
        
        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnTick()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}