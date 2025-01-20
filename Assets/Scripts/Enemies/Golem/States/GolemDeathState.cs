using UtilScripts;
using Zenject;

namespace Enemies.Golem.States
{
    public class GolemDeathState : GolemBaseState
    {
        protected GolemDeathState(GolemStateMachine golemStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(golemStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }
    }
}