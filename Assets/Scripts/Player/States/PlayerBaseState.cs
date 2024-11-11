using StateMachine;
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

        protected PlayerBaseState(PlayerStateMachine playerStateMachine, IInstantiator instantiator, SignalBus signalBus)
        {
            PlayerStateMachine = playerStateMachine;
            Instantiator = instantiator;
            SignalBus = signalBus;
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
