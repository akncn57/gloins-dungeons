using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Player.Commands
{
    public class PlayerFacingCommand : ICommand
    {
        private readonly PlayerFacing _playerFacing;
        private readonly GameObject _parentObject;
        private readonly float _horizontalMove;
        
        public PlayerFacingCommand(PlayerFacing playerFacing, GameObject parentObject, float horizontalMove)
        {
            _playerFacing = playerFacing;
            _parentObject = parentObject;
            _horizontalMove = horizontalMove;
        }

        public object Execute()
        {
            _playerFacing.Facing(_parentObject, _horizontalMove);
            return default;
        }

        public void Undo(){}
    }
}