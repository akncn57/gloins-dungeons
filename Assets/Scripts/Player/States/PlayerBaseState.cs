using StateMachine;
using UtilScripts;
using Zenject;

namespace Player.States
{
    [System.Serializable]
    public abstract class PlayerBaseState : BaseState
    {
        protected abstract PlayerStateEnums StateEnum { get; }
        protected PlayerStateMachine PlayerStateMachine;
        protected IInstantiator Instantiator;
        protected SignalBus SignalBus;
        protected CoroutineRunner CoroutineRunner;
        protected CameraShake CameraShake;

        protected PlayerBaseState(PlayerStateMachine playerStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake)
        {
            PlayerStateMachine = playerStateMachine;
            Instantiator = instantiator;
            SignalBus = signalBus;
            CoroutineRunner = coroutineRunner;
            CameraShake = cameraShake;
        }
    }
    
    public enum PlayerStateEnums
    {
        Idle,
        Walk,
        AttackBasic,
        Block,
        Hurt,
        Death
    }
}
