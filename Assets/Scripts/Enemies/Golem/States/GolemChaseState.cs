using UtilScripts;
using Zenject;

namespace Enemies.Golem.States
{
    public class GolemChaseState : GolemBaseState
    {
        protected GolemChaseState(GolemStateMachine golemStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(golemStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }
    }
}