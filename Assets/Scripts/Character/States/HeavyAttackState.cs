using StateMachine;

namespace Character.States
{
    public class HeavyAttackState : State<CharacterController>
    {
        public HeavyAttackState(CharacterController context, BaseStateMachine<CharacterController> stateMachine) : base(context, stateMachine) {}
    }
}