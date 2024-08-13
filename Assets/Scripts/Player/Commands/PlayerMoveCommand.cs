using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Player.Commands
{
    public class PlayerMoveCommand : ICommand
    {
        private readonly PlayerMover _playerMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly Vector2 _movement;
        private readonly float _speed;
        
        public PlayerMoveCommand(PlayerMover playerMover, Rigidbody2D rigidbody, Vector2 movement, float speed)
        {
            _playerMover = playerMover;
            _rigidbody = rigidbody;
            _movement = movement;
            _speed = speed;
        }
        
        public void Execute()
        {
            _playerMover.Move(_rigidbody, _movement, _speed);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}