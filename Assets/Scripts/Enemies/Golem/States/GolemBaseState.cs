using UtilScripts;
using Zenject;

namespace Enemies.Golem.States
{
    public class GolemBaseState : EnemyBaseState
    {
        protected GolemStateMachine GolemStateMachine;
        protected IInstantiator Instantiator;
        protected SignalBus SignalBus;
        protected CoroutineRunner CoroutineRunner;
        protected CameraShake CameraShake;
        
        protected GolemBaseState(GolemStateMachine golemStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake)
        {
            GolemStateMachine = golemStateMachine;
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