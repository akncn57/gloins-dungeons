namespace Zenject.Signals.Combat
{
    public struct TargetChangedSignal
    {
        public string EnemyName;
        public float CurrentHealth;
        public float MaxHealth;
        public bool HasTarget;
    }
}