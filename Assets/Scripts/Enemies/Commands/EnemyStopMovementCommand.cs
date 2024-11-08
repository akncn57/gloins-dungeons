using DesignPatterns.CommandPattern;
using UnityEngine.AI;

namespace Enemies.Commands
{
    public class EnemyStopMovementCommand : ICommand
    {
        private readonly EnemyStopMovement _enemyStopMovement;
        private readonly NavMeshAgent _agent;

        public EnemyStopMovementCommand(EnemyStopMovement enemyStopMovement, NavMeshAgent agent)
        {
            _enemyStopMovement = enemyStopMovement;
            _agent = agent;
        }
        
        public object Execute()
        {
            _enemyStopMovement.StopMovement(_agent);
            return default;
        }

        public void Undo() {}
    }
}