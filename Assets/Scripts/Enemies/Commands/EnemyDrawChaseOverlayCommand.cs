using DesignPatterns.CommandPattern;
using Enemies.Skeleton;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyDrawChaseOverlayCommand : ICommand
    {
        private readonly EnemyDrawChaseOverlay _enemyDrawChaseOverlay;
        private readonly Vector3 _position;
        private readonly float _radius;
        private readonly EnemyBaseStateMachine _enemyBaseStateMachine;
        
        public EnemyDrawChaseOverlayCommand(EnemyDrawChaseOverlay enemyDrawChaseOverlay, Vector3 position, float radius, EnemyBaseStateMachine enemyBaseStateMachine)
        {
            _enemyDrawChaseOverlay = enemyDrawChaseOverlay;
            _position = position;
            _radius = radius;
            _enemyBaseStateMachine = enemyBaseStateMachine;
        }
        
        public object Execute()
        {
            return _enemyDrawChaseOverlay.IsPlayerWithinRadius(_position, _radius, _enemyBaseStateMachine);
        }

        public void Undo(){}
    }
}