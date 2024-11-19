using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyStopRigidbodyCommand : ICommand
    {
        private readonly EnemyStopRigidbody _enemyStopRigidbody;
        private readonly Rigidbody2D _rigidbody;

        public EnemyStopRigidbodyCommand(EnemyStopRigidbody enemyStopRigidbody, Rigidbody2D rigidbody)
        {
            _enemyStopRigidbody = enemyStopRigidbody;
            _rigidbody = rigidbody;
        }
        
        public object Execute()
        {
            _enemyStopRigidbody.StopRigidbody(_rigidbody);
            return default;
        }

        public void Undo() {}
    }
}