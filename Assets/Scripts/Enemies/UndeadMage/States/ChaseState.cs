using UnityEngine;

namespace Enemies.UndeadMage.States
{
    public class ChaseState : UndeadMageBaseState
    {
        public ChaseState(UndeadMageController context, UndeadMageStateMachine stateMachine) : base(context, stateMachine) {}

        public override void Enter()
        {
            Context.Animator.SetBool(UndeadMageAnimatorHashes.IsWalking, true);
        }

        public override void Exit()
        {
            Context.Animator.SetBool(UndeadMageAnimatorHashes.IsWalking, false);
            Context.Rb.linearVelocity = Vector2.zero;
        }

        public override void FixedUpdate()
        {
            if (Context.PlayerTarget == null) return;

            HandleMovement();
        }
        
        public override void Update()
        {
            if (Context.PlayerTarget == null) return;

            CheckStateTransitions();
        }
        
        private void HandleMovement()
        {
            var distToPlayerX = Context.transform.position.x - Context.PlayerTarget.position.x;
            var distToPlayerY = Context.transform.position.y - Context.PlayerTarget.position.y;
            var absDistX = Mathf.Abs(distToPlayerX);
            
            var stats = (UndeadMageStatsSO)Context.EnemyStats;
            
            Vector2 moveDirection = Vector2.zero;
            
            // Y ekseninde oyuncuyla aynı hizaya gelmeye çalış
            if (Mathf.Abs(distToPlayerY) > 0.2f)
            {
                moveDirection.y = distToPlayerY > 0 ? -1 : 1;
            }
            
            // X ekseninde retreat veya safe distance ayarı
            if (absDistX < stats.RetreatDistance)
            {
                // Çok yakın, geriye doğru kaç
                moveDirection.x = distToPlayerX > 0 ? 1 : -1;
            }
            else if (absDistX > stats.LightAttackRange)
            {
                // Uzak, oyuncuya yaklaş
                moveDirection.x = distToPlayerX > 0 ? -1 : 1;
            }

            moveDirection = moveDirection.normalized;

            var separation = CalculateSeparation();
            moveDirection = (moveDirection + separation * 1.5f).normalized;

            if (moveDirection.magnitude > 0.1f && (absDistX < stats.RetreatDistance || absDistX > stats.LightAttackRange || Mathf.Abs(distToPlayerY) > 0.2f))
            {
                Context.Rb.linearVelocity = moveDirection * Context.EnemyStats.MoveSpeed;
                
                // Geri geri kaçarken oyuncuya bakmasını sağla
                FacePlayer();
                
                Context.Animator.SetBool(UndeadMageAnimatorHashes.IsWalking, true);
            }
            else
            {
                // Saldırı menzilinde dur
                Context.Rb.linearVelocity = Vector2.zero;
                FacePlayer();
                Context.Animator.SetBool(UndeadMageAnimatorHashes.IsWalking, false);
            }
        }

        private Vector2 CalculateSeparation()
        {
            Vector2 separation = Vector2.zero;
            int count = 0;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Context.transform.position, 1.5f);
            
            foreach (var col in colliders)
            {
                if (col.gameObject == Context.gameObject) continue;

                if (col.TryGetComponent<EnemyBase>(out _))
                {
                    Vector2 awayFromEnemy = Context.transform.position - col.transform.position;
                    float distance = awayFromEnemy.magnitude;
                    if (distance < 0.1f) distance = 0.1f;
                    
                    separation += awayFromEnemy.normalized / distance; 
                    count++;
                }
            }
            
            if (count > 0)
            {
                separation /= count;
            }
            
            return separation;
        }

        private void FacePlayer()
        {
            Context.SpriteRenderer.flipX = Context.PlayerTarget.position.x < Context.transform.position.x;
        }
        
        private void CheckStateTransitions()
        {
            var stats = (UndeadMageStatsSO)Context.EnemyStats;
            var absDistX = Mathf.Abs(Context.transform.position.x - Context.PlayerTarget.position.x);
            var absDistY = Mathf.Abs(Context.transform.position.y - Context.PlayerTarget.position.y);
            
            if (absDistY <= 0.5f)
            {
                FacePlayer();

                if (absDistX <= stats.HeavyAttackRange)
                {
                    if (Time.time >= Context.LastHeavyAttackTime + stats.HeavyAttackCooldown)
                    {
                        StateMachine.ChangeState(UndeadMageStateMachine.HeavyAttackState);
                        return;
                    }
                }
                else if (absDistX <= stats.LightAttackRange && absDistX >= stats.RetreatDistance)
                {
                    if (Time.time >= Context.LastLightAttackTime + stats.LightAttackCooldown)
                    {
                        StateMachine.ChangeState(UndeadMageStateMachine.LightAttackState);
                        return;
                    }
                }
            }

            if (HasTargetEscaped())
            {
                StateMachine.ChangeState(UndeadMageStateMachine.IdleState);
            }
        }

        private bool HasTargetEscaped()
        {
            var distToPlayer = Vector2.Distance(Context.transform.position, Context.PlayerTarget.position);
            return distToPlayer > Context.EnemyStats.ChaseRange;
        }
    }
}
