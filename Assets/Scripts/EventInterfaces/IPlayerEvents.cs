namespace EventInterfaces
{
    public interface IPlayerEvents
    {
        public class OnPlayerHealthChanged
        {
            public long TempHealth;
            public long CurrentHealth;
        }
        
        public class OnPlayerHealthLimitChanged
        {
            public long CurrentHealthLimit;
        }

        public class OnPlayerAttacked
        {
            public bool IsAttacked;
        }
    }
}