using UtilScripts;
using Zenject;

namespace Enemies.Golem.States
{
    public class GolemHurtState : GolemBaseState
    {
        protected GolemHurtState(GolemStateMachine golemStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(golemStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }
    }
}