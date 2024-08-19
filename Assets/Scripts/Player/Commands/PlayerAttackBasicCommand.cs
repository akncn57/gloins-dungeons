using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Player.Commands
{
    public class PlayerAttackBasicCommand : ICommand
    {
        private readonly PlayerAttackBasic _playerAttackBasic;
        private readonly CapsuleCollider2D _attackCollider;
        private readonly int _attackPower;
        private readonly float _hitKnockBackPower;
        private readonly Vector3 _playerPosition;
        
        public PlayerAttackBasicCommand(
            PlayerAttackBasic playerAttackBasic,
            CapsuleCollider2D attackCollider,
            int attackPower,
            float hitKnockBackPower,
            Vector3 playerPosition)
        {
            _playerAttackBasic = playerAttackBasic;
            _attackCollider = attackCollider;
            _attackPower = attackPower;
            _hitKnockBackPower = hitKnockBackPower;
            _playerPosition = playerPosition;
        }

        public object Execute()
        {
            _playerAttackBasic.PlayerOnAttackBasicOpenOverlap(_attackCollider, _attackPower, _hitKnockBackPower, _playerPosition);
            return default;
        }

        public void Undo()
        {
            _playerAttackBasic.PlayerOnAttackBasicCloseOverlap();
        }
    }
}