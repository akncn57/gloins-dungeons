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

            HandleMovement();
        }
        
        public override void Update()
        {
            if (Context.PlayerTarget == null) return;

            CheckStateTransitions();
        }
        
        private void HandleMovement()
        {
            var targetPos = CalculateFlankPosition();
            var distanceToTarget = (targetPos - (Vector2)Context.transform.position);
            
            var moveDirection = distanceToTarget.normalized;
            var separation = CalculateSeparation();
            
            // Add separation to avoid pushing other enemies and bulldozing the player
            moveDirection = (moveDirection + separation * 1.5f).normalized;

            if (distanceToTarget.magnitude > 0.1f)
            {
                Context.Rb.linearVelocity = moveDirection * Context.EnemyStats.MoveSpeed;
                UpdateFacingDirection(distanceToTarget.x < 0);
            }
            else
            {
                Context.Rb.linearVelocity = Vector2.zero;
                FacePlayer();
            }
        }

        private Vector2 CalculateSeparation()
        {
            Vector2 separation = Vector2.zero;
            int count = 0;
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Context.transform.position, 1.5f);
            var stats = (UndeadSwordsmanStatsSO)Context.EnemyStats;
            
            foreach (var col in colliders)
            {
                if (col.gameObject == Context.gameObject) continue;

                // Separate from other enemies so they don't clump up and push each other
                if (col.TryGetComponent<EnemyBase>(out _))
                {
                    Vector2 awayFromEnemy = Context.transform.position - col.transform.position;
                    float distance = awayFromEnemy.magnitude;
                    if (distance < 0.1f) distance = 0.1f;
                    
                    separation += awayFromEnemy.normalized / distance; 
                    count++;
                }
                // Separate from player if getting too close to prevent pushing them
                else if (col.CompareTag("Player"))
                {
                    Vector2 awayFromPlayer = Context.transform.position - col.transform.position;
                    float distance = awayFromPlayer.magnitude;
                    
                    if (distance < stats.FlankDistance * 0.9f) 
                    {
                        if (distance < 0.1f) distance = 0.1f;
                        // Stronger separation for player to ensure no bulldozing
                        separation += (awayFromPlayer.normalized / distance) * 2.0f; 
                        count++;
                    }
                }
            }
            
            if (count > 0)
            {
                separation /= count;
            }
            
            return separation;
        }

        private Vector2 CalculateFlankPosition()
        {
            var flankDist = ((UndeadSwordsmanStatsSO)Context.EnemyStats).FlankDistance;
            var targetX = Context.transform.position.x < Context.PlayerTarget.position.x 
                ? Context.PlayerTarget.position.x - flankDist 
                : Context.PlayerTarget.position.x + flankDist;
                
            return new Vector2(targetX, Context.PlayerTarget.position.y);
        }

        private void UpdateFacingDirection(bool lookLeft)
        {
            Context.SpriteRenderer.flipX = lookLeft;
        }

        private void FacePlayer()
        {
            Context.SpriteRenderer.flipX = Context.PlayerTarget.position.x < Context.transform.position.x;
        }
        
        private void CheckStateTransitions()
        {
            if (IsWithinAttackRange())
            {
                FacePlayer();

                var stats = (UndeadSwordsmanStatsSO)Context.EnemyStats;
                
                // Attack cooldown check
                var cooldown = stats.LightAttackAttackCooldown; // Using light as baseline or heavy if separated later
                if (Time.time >= Context.LastAttackTime + cooldown)
                {
                    var doHeavyAttack = Random.value <= stats.HeavyAttackChance;

                    if (doHeavyAttack)
                    {
                        StateMachine.ChangeState(UndeadSwordsmanStateMachine.HeavyAttackState);
                    }
                    else
                    {
                        StateMachine.ChangeState(UndeadSwordsmanStateMachine.LightAttackState);
                    }
                }
            }
            else if (HasTargetEscaped())
            {
                StateMachine.ChangeState(UndeadSwordsmanStateMachine.IdleState);
            }
        }

        private bool IsWithinAttackRange()
        {
            var flankDist = ((UndeadSwordsmanStatsSO)Context.EnemyStats).FlankDistance;
            var inXRange = Mathf.Abs(Context.transform.position.x - Context.PlayerTarget.position.x) <= flankDist + 0.2f;
            var inYRange = Mathf.Abs(Context.transform.position.y - Context.PlayerTarget.position.y) <= 0.2f;
            
            return inXRange && inYRange;
        }

        private bool HasTargetEscaped()
        {
            var distToPlayer = Vector2.Distance(Context.transform.position, Context.PlayerTarget.position);
            return distToPlayer > Context.EnemyStats.ChaseRange;
        }
    }
}
