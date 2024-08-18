using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyStopMoveCommand : ICommand
    {
        private readonly EnemyMover _enemyMover;
        private readonly Rigidbody2D _rigidbody;
        
        public EnemyStopMoveCommand(EnemyMover enemyMover, Rigidbody2D rigidbody)
        {
            _enemyMover = enemyMover;
            _rigidbody = rigidbody;
        }
        
        public void Execute()
        {
            _enemyMover.Stop(_rigidbody);
        }

        public void Undo(){}
    }
}