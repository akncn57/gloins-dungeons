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

        protected PlayerBaseState(PlayerStateMachine playerStateMachine, IInstantiator instantiator)
        {
            PlayerStateMachine = playerStateMachine;
            Instantiator = instantiator;
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
