using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Player.Commands
{
    public class PlayerDashCommand : ICommand
    {
        private readonly PlayerMover _playerMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _dashDirectionX;
        private readonly float _force;
        
        public PlayerDashCommand(PlayerMover playerMover, Rigidbody2D rigidbody, float dashDirectionX, float force)
        {
            _playerMover = playerMover;
            _rigidbody = rigidbody;
            _dashDirectionX = dashDirectionX;
            _force = force;
        }
        
        public object Execute()
        {
            _playerMover.Dash(_rigidbody, _dashDirectionX, _force);
            return default;
        }

        public void Undo(){}
    }
}