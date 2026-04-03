using UnityEngine;

namespace Enemies.UndeadSwordsman.States
{
    public class ChaseState : UndeadSwordsmanBaseState
    {
        public ChaseState(UndeadSwordsmanController context, UndeadSwordsmanStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetBool(UndeadSwordsmanAnimatorHashes.IsWalking, true);
        }

        public override void Exit()
        {
            Context.Animator.SetBool(UndeadSwordsmanAnimatorHashes.IsWalking, false);
            Context.Rb.linearVelocity = Vector2.zero;
        }

        public override void FixedUpdate()
        {
            if (Context.PlayerTarget == null) return;
            
            // FLANKING MANTIĞI: Oyuncunun sağına mı soluna mı gitmeliyiz?
            // Düşman oyuncunun solundaysa sol hizaya (Player.x - Range)
            // Sağındaysa sağ hizaya gidecek. Y ekseninde ise tam oyuncu ile hizalanacak.
            float flankDist = ((UndeadSwordsmanStatsSO)Context.EnemyStats).FlankDistance;
            float targetX = Context.transform.position.x < Context.PlayerTarget.position.x 
                ? Context.PlayerTarget.position.x - flankDist 
                : Context.PlayerTarget.position.x + flankDist;
                
            Vector2 targetPos = new Vector2(targetX, Context.PlayerTarget.position.y);
            
            // Yöne doğru hareket
            Vector2 direction = (targetPos - (Vector2)Context.transform.position);
            
            if (direction.magnitude > 0.1f)
            {
                Context.Rb.linearVelocity = direction.normalized * Context.EnemyStats.MoveSpeed;
                
                // Karakteri yürüdüğü yöne çevir
                Context.SpriteRenderer.flipX = Context.Rb.linearVelocity.x < 0;
            }
            else
            {
                Context.Rb.linearVelocity = Vector2.zero;
                // Eğer ulaştıysak yüzümüzü oyuncuya dönelim
                Context.SpriteRenderer.flipX = Context.PlayerTarget.position.x < Context.transform.position.x;
            }
        }

        public override void Update()
        {
            if (Context.PlayerTarget == null) return;

            float distToPlayer = Vector2.Distance(Context.transform.position, Context.PlayerTarget.position);
            float flankDist = ((UndeadSwordsmanStatsSO)Context.EnemyStats).FlankDistance;
            
            // X ekseninde uygun mesafede mi ve Y ekseninde (dikine) hemen hemen aynı hizada mı?
            bool inXRange = Mathf.Abs(Context.transform.position.x - Context.PlayerTarget.position.x) <= flankDist + 0.2f;
            bool inYRange = Mathf.Abs(Context.transform.position.y - Context.PlayerTarget.position.y) <= 0.2f;

            if (inXRange && inYRange)
            {
                Context.SpriteRenderer.flipX = Context.PlayerTarget.position.x < Context.transform.position.x;
                StateMachine.ChangeState(UndeadSwordsmanStateMachine.LightAttackState);
            }
            // Oyuncu ChaseRange'den çıktıysa kovalamayı bırak
            else if (distToPlayer > Context.EnemyStats.ChaseRange)
            {
                StateMachine.ChangeState(UndeadSwordsmanStateMachine.IdleState);
            }
        }
    }
}
