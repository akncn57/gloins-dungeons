using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Player.Commands
{
    public class PlayerAttackCommand : ICommand
    {
        private readonly PlayerAttack _playerAttack;
        private readonly BoxCollider2D _attackCollider;
        private readonly int _attackPower;
        private readonly float _hitKnockBackPower;
        private readonly Vector3 _playerPosition;
        
        public PlayerAttackCommand(
            PlayerAttack playerAttack,
            BoxCollider2D attackCollider,
            int attackPower,
            float hitKnockBackPower,
            Vector3 playerPosition)
        {
            _playerAttack = playerAttack;
            _attackCollider = attackCollider;
            _attackPower = attackPower;
            _hitKnockBackPower = hitKnockBackPower;
            _playerPosition = playerPosition;
        }

        public object Execute()
        {
            _playerAttack.PlayerOnAttackOpenOverlap(_attackCollider, _attackPower, _hitKnockBackPower, _playerPosition);
            return default;
        }

        public void Undo()
        {
            _playerAttack.PlayerOnAttackCloseOverlap();
        }
    }
}