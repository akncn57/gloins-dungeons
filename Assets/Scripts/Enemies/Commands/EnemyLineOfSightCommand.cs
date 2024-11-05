using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyLineOfSightCommand : ICommand
    {
        private readonly EnemyLineOfSight _enemyLineOfSight;
        private readonly Collider2D _enemyPosition;
        private readonly Collider2D _playerPosition;
        private readonly string _playerTag;
    
        public object Execute()
        {
            return _enemyLineOfSight.HasLineOfSight(_enemyPosition, _playerPosition, _playerTag, new LayerMask());
        }

        public void Undo()
        {
        
        }
    }
}
