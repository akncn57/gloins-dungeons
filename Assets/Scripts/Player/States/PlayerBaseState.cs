using StateMachine;

namespace Player.States
{
    [System.Serializable]
    public abstract class PlayerBaseState : BaseState
    {
        protected abstract PlayerStateEnums StateEnum { get; }
        protected PlayerStateMachine PlayerStateMachine;

        protected PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;
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
