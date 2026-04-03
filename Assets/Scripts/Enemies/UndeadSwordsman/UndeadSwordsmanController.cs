namespace Enemies.UndeadSwordsman
{
    public class UndeadSwordsmanController : EnemyBase
    {
        protected override void Awake()
        {
            StateMachine = new UndeadSwordsmanStateMachine(this);
            // 1. Base sınıftaki Awake'i çağırırız. Bu, Rb, Animator gibi bileşenleri alır 
            // ve StateMachine'i (EnemyStateMachine) otomatik olarak oluşturur!
            base.Awake();

            // 3. Makineyi Idle state ile başlatıyoruz.
            // StateMachine.Initialize(StateMachine.IdleState);
        }
    }
}