﻿using DesignPatterns.CommandPattern;
using UnityEngine;

namespace Enemies.Commands
{
    public class EnemyKnockbackCommand : ICommand
    {
        private readonly EnemyKnockback _enemyKnockback;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _hitPositionX;
        private readonly float _knockBackStrength;
        
        public EnemyKnockbackCommand(EnemyKnockback enemyKnockback, Rigidbody2D rigidbody, float hitPositionX, float knockBackStrength)
        {
            _enemyKnockback = enemyKnockback;
            _rigidbody = rigidbody;
            _hitPositionX = hitPositionX;
            _knockBackStrength = knockBackStrength;
        }
        
        public object Execute()
        {
            _enemyKnockback.KnockBack(_rigidbody, _hitPositionX, _knockBackStrength);
            return default;
        }

        public void Undo() {}
    }
}