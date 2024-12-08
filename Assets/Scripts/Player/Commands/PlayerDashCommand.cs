using DesignPatterns.CommandPattern;
using UnityEngine;
using UtilScripts;

namespace Player.Commands
{
    public class PlayerDashCommand : ICommand
    {
        private readonly PlayerMover _playerMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly Vector2 _dashDirection;
        private readonly float _force;
        
        public PlayerDashCommand(PlayerMover playerMover, Rigidbody2D rigidbody, Vector2 dashDirection, float force)
        {
            _playerMover = playerMover;
            _rigidbody = rigidbody;
            _dashDirection = dashDirection;
            _force = force;
        }
        
        public object Execute()
        {
            _playerMover.Dash(_rigidbody, _dashDirection, _force);
            //.TryStartCoroutine(_playerMover.DashCor(_rigidbody, _dashDirection, _force, 1.5f));
            return default;
        }

        public void Undo(){}
    }
}