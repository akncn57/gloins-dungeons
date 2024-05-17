using HealthSystem;

namespace DesignPatterns.CommandPattern.CustomCommands
{
    public class SpendHealthCommand : ICommand
    {
        private readonly HealthController _healthController;
        private readonly int _spendAmount;
        private long _previousHealth;
        
        public SpendHealthCommand(HealthController healthController, int spendAmount)
        {
            _healthController = healthController;
            _spendAmount = spendAmount;
        }
        
        public void Execute()
        {
            _previousHealth = _healthController.HealthData.Health;
            _healthController.SpendHealth(_spendAmount);
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
            //TODO: Add HealthSet method in HealthController.
        }
    }
}