using DesignPatterns.CommandPattern;
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
        
        public void Execute()
        {
            _healthController.SpendHealth(_damage);
        }

        public void Undo(){}
    }
}