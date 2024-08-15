using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Player.Commands
{
    public class PlayerStopMoveCommand : ICommand
    {
        private PlayerMover _playerMover;
        private Rigidbody2D _rigidbody;
        
        public PlayerStopMoveCommand(PlayerMover playerMover, Rigidbody2D rigidbody)
        {
            _playerMover = playerMover;
            _rigidbody = rigidbody;
        }
        
        public void Execute()
        {
            _playerMover.Stop(_rigidbody);
        }

        public void Undo(){}
    }
}