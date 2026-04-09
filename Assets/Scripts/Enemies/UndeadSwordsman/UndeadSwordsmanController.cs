namespace Enemies.UndeadSwordsman
{
    public class UndeadSwordsmanController : EnemyBase
    {
        public float LastAttackTime { get; set; } = -100f;
        
        private UndeadSwordsmanStateMachine _undeadSwordsmanStateMachine;

        protected override void Awake()
        {
            _undeadSwordsmanStateMachine = new UndeadSwordsmanStateMachine(this);
            StateMachine = _undeadSwordsmanStateMachine;
            base.Awake();
        }

        public void OnLightAttackAnimationEnd()
        {
            _undeadSwordsmanStateMachine.OnLightAttackAnimationEnd();
        }

        public void OnHeavyAttackAnimationEnd()
        {
            _undeadSwordsmanStateMachine.OnHeavyAttackAnimationEnd();
        }

        public void OnHurtAnimationEnd()
        {
            _undeadSwordsmanStateMachine.OnHurtAnimationEnd();
        }
    }
}