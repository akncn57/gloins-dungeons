﻿using DesignPatterns.CommandPattern;
using HealthSystem;

namespace Enemies.Commands
{
    public class EnemySpendHealthCommand : ICommand
    {
        private readonly HealthController _healthController;
        private readonly int _damage;
        
        public EnemySpendHealthCommand(HealthController healthController, int damage)
        {
            _healthController = healthController;
            _damage = damage;
        }
        
        public object Execute()
        {
            _healthController.SpendHealth(_damage);
            return default;
        }

        public void Undo(){}
    }
}