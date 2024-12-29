using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcPatrolState : OrcBaseState
    {
        protected OrcPatrolState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        public override void OnEnter()
        {
            
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}