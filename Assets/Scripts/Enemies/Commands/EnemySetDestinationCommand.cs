using DesignPatterns.CommandPattern;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Commands
{
    public class EnemySetDestinationCommand : ICommand
    {
        private readonly EnemySetDestination _enemySetDestination;
        private readonly NavMeshAgent _agent;
        private readonly Vector3 _targetPosition;

        public EnemySetDestinationCommand(EnemySetDestination enemySetDestination, NavMeshAgent agent, Vector3 targetPosition)
        {
            _enemySetDestination = enemySetDestination;
            _agent = agent;
            _targetPosition = targetPosition;
        }

        public object Execute()
        {
            _enemySetDestination.SetDestination(_agent, _targetPosition);
            return default;
        }

        public void Undo() {}
    }
}