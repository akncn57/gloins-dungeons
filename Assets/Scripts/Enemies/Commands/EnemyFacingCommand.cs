using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyFacingCommand : ICommand
    {
        private readonly EnemyFacing _enemyFacing;
        private readonly GameObject _parentObject;
        private readonly float _horizontalMove;
        
        public EnemyFacingCommand(EnemyFacing enemyFacing, GameObject parentObject, float horizontalMove)
        {
            _enemyFacing = enemyFacing;
            _parentObject = parentObject;
            _horizontalMove = horizontalMove;
        }

        public void Execute()
        {
            _enemyFacing.Facing(_parentObject, _horizontalMove);
        }

        public void Undo(){}
    }
}