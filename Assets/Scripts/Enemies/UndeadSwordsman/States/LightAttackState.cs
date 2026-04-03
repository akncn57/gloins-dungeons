namespace Enemies.UndeadSwordsman.States
{
    public class LightAttackState : UndeadSwordsmanBaseState
    {
        private float _attackTimer;

        public LightAttackState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetTrigger(UndeadSwordsmanAnimatorHashes.LightAttack);
            Context.Rb.linearVelocity = UnityEngine.Vector2.zero;
            
            // Düşmanın vurma bekleme süresini stat dosyasından çekip başlatıyoruz
            _attackTimer = ((UndeadSwordsmanStatsSO)Context.EnemyStats).LightAttackAttackCooldown;
        }

        public override void Update()
        {
            _attackTimer -= UnityEngine.Time.deltaTime;

            if (_attackTimer <= 0)
            {
                // Saldırı bitti! Eğer oyuncu kaçmışsa tekrar kovalaması için State makinesini resetliyoruz.
                // IdleState otomatik olarak menzil kontrolü yapacak ve uzaktaysa ChaseState'e geçecektir.
                StateMachine.ChangeState(UndeadSwordsmanStateMachine.IdleState);
            }
        }
    }
}