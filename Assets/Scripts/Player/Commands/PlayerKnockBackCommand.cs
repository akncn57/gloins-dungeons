using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Player.Commands
{
    public class PlayerKnockBackCommand : ICommand
    {
        private readonly PlayerMover _playerMover;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _hitPositionX;
        private readonly float _knockBackStrength;
        
        public PlayerKnockBackCommand(PlayerMover playerMover, Rigidbody2D rigidbody, float hitPositionX, float knockBackStrength)
        {
            _playerMover = playerMover;
            _rigidbody = rigidbody;
            _hitPositionX = hitPositionX;
            _knockBackStrength = knockBackStrength;
        }
        
        public void Execute()
        {
            _playerMover.KnockBack(_rigidbody, _hitPositionX, _knockBackStrength);
        }

        public void Undo(){}
    }
}