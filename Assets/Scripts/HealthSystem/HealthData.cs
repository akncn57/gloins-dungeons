namespace HealthSystem
{
    public sealed class HealthData
    {
        public long Health;
        public long HealthLimit;

        public HealthData(long health, long healthLimit)
        {
            Health = health;
            HealthLimit = healthLimit;
        }
    }
}