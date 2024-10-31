using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyLineOfSightCommand : ICommand
    {
        private readonly EnemyLineOfSight _enemyLineOfSight;
        private readonly Vector2 _enemyPosition;
        private readonly Vector2 _playerPosition;
        private readonly string _playerTag;
    
        public object Execute()
        {
            return _enemyLineOfSight.HasLineOfSight(_enemyPosition, _playerPosition, _playerTag);
        }

        public void Undo()
        {
        
        }
    }
}
