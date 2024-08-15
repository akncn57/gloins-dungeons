using DesignPatterns.CommandPattern;
using HealthSystem;

namespace Player.Commands
{
    public class PlayerHurtCommand : ICommand
    {
        private readonly HealthController _healthController;
        private readonly int _damage;
        
        public PlayerHurtCommand(HealthController healthController, int damage)
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