using StateMachine;

namespace Enemies
{
    public abstract class EnemyStateBase : State<EnemyBase>
    {
        protected readonly EnemyStateMachine EnemyStateMachine;

        protected EnemyStateBase(EnemyBase context, EnemyStateMachine stateMachine) : base(context, stateMachine)
        {
            EnemyStateMachine = stateMachine;
        }
    }
}