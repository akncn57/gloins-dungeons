using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyKnockBackCommand : ICommand
    {
        private readonly EnemyMover _enemyMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _hitPositionX;
        private readonly float _knockBackStrength;
        
        public EnemyKnockBackCommand(EnemyMover enemyMover, Rigidbody2D rigidbody, float hitPositionX, float knockBackStrength)
        {
            _enemyMover = enemyMover;
            _rigidbody = rigidbody;
            _hitPositionX = hitPositionX;
            _knockBackStrength = knockBackStrength;
        }
        
        public void Execute()
        {
            _enemyMover.KnockBack(_rigidbody, _hitPositionX, _knockBackStrength);
        }

        public void Undo(){}
    }
}