using System.Collections.Generic;
using System.Linq;
using ColliderController;
using UnityEngine;
using UtilScripts;
using Zenject;

namespace Enemies.Orc.States
{
    public class OrcBasicAttackState : OrcBaseState
    {
        private static readonly int BasicAttackAnimationHash = Animator.StringToHash("AttackBasic-BlendTree");
        private readonly List<ColliderControllerBase> _hittingEnemies = new(); // List of enemies hit during the attack
        private float _lastAttackTime; // Tracks the time of the last attack
        
        protected OrcBasicAttackState(
            OrcStateMachine orcStateMachine, 
            IInstantiator instantiator, 
            SignalBus signalBus, 
            CoroutineRunner coroutineRunner, 
            CameraShake cameraShake) 
            : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        /// <summary>
        /// Called when entering the basic attack state. 
        /// </summary>
        public override void OnEnter()
        {
            OrcStateMachine.EnemyColliderController.OnHitStart += CheckHurt;
            
            if (IsAttackOnCooldown())
            {
                OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
                return;
            }

            StartAttack();
        }
        
        /// <summary>
        /// Called once per frame during the basic attack state.
        /// </summary>
        public override void OnTick()
        {
        }

        /// <summary>
        /// Called when exiting the basic attack state.
        /// </summary>
        public override void OnExit()
        {
            OrcStateMachine.EnemyColliderController.OnHitStart -= CheckHurt;
            
            UnregisterAnimationEvents();
        }

        /// <summary>
        /// Opens the attack collision window and detects enemies in the attack area.
        /// </summary>
        private void OrcAttackOpenOverlap()
        {
            var detectedEnemies = DetectEnemiesInAttackArea();
            _hittingEnemies.Clear();

            foreach (var enemy in detectedEnemies)
            {
                if (enemy == null) continue;

                _hittingEnemies.Add(enemy);
                enemy.InvokeOnHitStartEvent(
                    OrcStateMachine.EnemyProperties.BasicAttackPower,
                    GetKnockbackDirection(enemy),
                    OrcStateMachine.EnemyProperties.HitKnockBackPower);
            }
        }

        /// <summary>
        /// Closes the attack collision window and resolves hit events.
        /// </summary>
        private void OrcAttackCloseOverlap()
        {
            foreach (var enemy in _hittingEnemies.Where(enemy => enemy != null))
            {
                enemy.InvokeOnHitEndEvent();
            }

            _hittingEnemies.Clear();
        }

        /// <summary>
        /// Completes the attack and transitions back to the idle state.
        /// </summary>
        private void OrcAttackFinish()
        {
            OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
        }

        /// <summary>
        /// Determines if the attack is on cooldown.
        /// </summary>
        /// <returns>True if the attack is on cooldown, false otherwise.</returns>
        private bool IsAttackOnCooldown()
        {
            return Time.time - _lastAttackTime < OrcStateMachine.EnemyProperties.BasicAttackCoolDown;
        }

        /// <summary>
        /// Starts the attack by resetting the cooldown timer and triggering the attack animation.
        /// </summary>
        private void StartAttack()
        {
            _lastAttackTime = Time.time;

            OrcStateMachine.Animator.Play(BasicAttackAnimationHash);
            RegisterAnimationEvents();
        }

        /// <summary>
        /// Registers animation events for the attack state.
        /// </summary>
        private void RegisterAnimationEvents()
        {
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapOpen += OrcAttackOpenOverlap;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapClose += OrcAttackCloseOverlap;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished += OrcAttackFinish;
        }

        /// <summary>
        /// Unregisters animation events to prevent memory leaks.
        /// </summary>
        private void UnregisterAnimationEvents()
        {
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapOpen -= OrcAttackOpenOverlap;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapClose -= OrcAttackCloseOverlap;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished -= OrcAttackFinish;
        }

        /// <summary>
        /// Detects enemies within the attack collider's range.
        /// </summary>
        /// <returns>A list of detected enemies.</returns>
        private IEnumerable<ColliderControllerBase> DetectEnemiesInAttackArea()
        {
            var detectedEnemies = new Collider2D[20];
            var contactFilter = new ContactFilter2D
            {
                useTriggers = true
            };

            contactFilter.SetLayerMask(OrcStateMachine.PlayerLayer);
            var count = Physics2D.OverlapCollider(GetAttackCollider(), contactFilter, detectedEnemies);

            for (var i = 0; i < count; i++)
            {
                yield return detectedEnemies[i]?.GetComponent<ColliderControllerBase>();
            }
        }

        /// <summary>
        /// Determines the direction of knockback for a hit enemy.
        /// </summary>
        /// <param name="enemy">The enemy to calculate knockback direction for.</param>
        /// <returns>A normalized vector indicating the knockback direction.</returns>
        private Vector2 GetKnockbackDirection(ColliderControllerBase enemy)
        {
            return (enemy.transform.position - OrcStateMachine.transform.position).normalized;
        }

        /// <summary>
        /// Retrieves the appropriate attack collider based on the Orc's current orientation.
        /// </summary>
        /// <returns>The selected attack collider.</returns>
        private Collider2D GetAttackCollider()
        {
            var attackHorizontal = OrcStateMachine.Animator.GetFloat("LastHorizontal");
            var attackVertical = OrcStateMachine.Animator.GetFloat("LastVertical");

            if (Mathf.Approximately(attackHorizontal, 1))
                return OrcStateMachine.BasicAttackColliderRight;

            if (Mathf.Approximately(attackHorizontal, -1))
                return OrcStateMachine.BasicAttackColliderLeft;

            if (Mathf.Approximately(attackVertical, 1))
                return OrcStateMachine.BasicAttackColliderUp;

            if (Mathf.Approximately(attackVertical, -1))
                return OrcStateMachine.BasicAttackColliderDown;

            return null;
        }
        
        private void CheckHurt(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            OrcStateMachine.SwitchState(OrcStateMachine.OrcHurtState);
        }
    }
}