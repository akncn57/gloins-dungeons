namespace Enemies.UndeadSwordsman
{
    public class UndeadSwordsmanController : EnemyBase
    {
        public UndeadSwordsmanCombatController CombatController { get; private set; }
        public float LastAttackTime { get; set; } = -100f;
        
        private UndeadSwordsmanStateMachine _undeadSwordsmanStateMachine;

        protected override void Awake()
        {
            CombatController = GetComponent<UndeadSwordsmanCombatController>();
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