using StateMachine;

namespace Enemies
{
    public class EnemyStateMachine : BaseStateMachine<EnemyBase>
    {
        public State<EnemyBase> HurtState { get; protected set; }
        public State<EnemyBase> DeathState { get; protected set; }
    }
}