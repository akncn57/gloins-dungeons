using UtilScripts;
using Zenject;

namespace Enemies.Golem.States
{
    public class GolemBasicAttackState : GolemBaseState
    {
        protected GolemBasicAttackState(GolemStateMachine golemStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(golemStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }
    }
}