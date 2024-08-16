using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyMoveCommand : ICommand
    {
        private readonly EnemyMover _enemyMover;
        private readonly Vector3 _playerPosition;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _speed;
        
        public EnemyMoveCommand(EnemyMover enemyMover, Vector3 playerPosition, Rigidbody2D rigidbody, float speed)
        {
            _enemyMover = enemyMover;
            _playerPosition = playerPosition;
            _rigidbody = rigidbody;
            _speed = speed;
        }
        
        public void Execute()
        {
            _enemyMover.Move(_playerPosition, _rigidbody, _speed);
        }

        public void Undo(){}
    }
}