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
        private static readonly int BasicAttackAnimationHash = Animator.StringToHash("AttackBasic_BlendTree");
        private readonly List<ColliderControllerBase> _hittingEnemies = new();
        
        private float _lastAttackTime;
        
        protected OrcBasicAttackState(OrcStateMachine orcStateMachine, IInstantiator instantiator, SignalBus signalBus, CoroutineRunner coroutineRunner, CameraShake cameraShake) : base(orcStateMachine, instantiator, signalBus, coroutineRunner, cameraShake)
        {
        }

        public override void OnEnter()
        {
            if (Time.time - _lastAttackTime < OrcStateMachine.EnemyProperties.BasicAttackCoolDown)
            {
                OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
                return;
            }

            _lastAttackTime = Time.time;
            
            OrcStateMachine.Animator.Play(BasicAttackAnimationHash);
            
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapOpen += OrcAttackOpenOverlap;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapClose += OrcAttackCloseOverlap;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished += OrcAttackFinish;
        }

        private void OrcAttackOpenOverlap()
        {
            var detectedEnemies = new Collider2D[20];
            var contactFilter = new ContactFilter2D();
            
            contactFilter.SetLayerMask(OrcStateMachine.PlayerLayer);
            contactFilter.useTriggers = true;
            
            var count = Physics2D.OverlapCollider(GetAttackCollider(), contactFilter, detectedEnemies);
            
            _hittingEnemies.Clear();
            
            for (var i = 0; i < count; i++)
            {
                var enemy = detectedEnemies[i].GetComponent<ColliderControllerBase>();
                
                if (enemy != null)
                {
                    _hittingEnemies.Add(enemy);
                    enemy.InvokeOnHitStartEvent(OrcStateMachine.EnemyProperties.BasicAttackPower, 
                        (enemy.transform.position - OrcStateMachine.transform.position).normalized, 
                        OrcStateMachine.EnemyProperties.HitKnockBackPower);
                }
            }
        }
        
        private void OrcAttackCloseOverlap()
        {
            foreach (var enemy in _hittingEnemies.Where(enemy => enemy))
            {
                enemy.InvokeOnHitEndEvent();
            }

            _hittingEnemies.Clear();
        }
        
        private void OrcAttackFinish()
        {
            OrcStateMachine.SwitchState(OrcStateMachine.OrcIdleState);
        }

        public override void OnTick()
        {
            // Saldırı sırasında yapılacak ek işler buraya eklenebilir.
        }

        public override void OnExit()
        {
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapOpen -= OrcAttackOpenOverlap;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapClose -= OrcAttackCloseOverlap;
            OrcStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished -= OrcAttackFinish;
        }

        private Collider2D GetAttackCollider()
        {
            var attackHorizontal = OrcStateMachine.Animator.GetFloat("LastHorizontal");
            var attackVertical = OrcStateMachine.Animator.GetFloat("LastVertical");
            
            if (Mathf.Approximately(attackHorizontal, 1))
            {
                return OrcStateMachine.BasicAttackColliderRight;
            }
            if (Mathf.Approximately(attackHorizontal, -1))
            {
                return OrcStateMachine.BasicAttackColliderLeft;
            }
            if (Mathf.Approximately(attackVertical, 1))
            {
                return OrcStateMachine.BasicAttackColliderUp;
            }
            if (Mathf.Approximately(attackVertical, -1))
            {
                return OrcStateMachine.BasicAttackColliderDown;
            }

            return null;
        }
    }
}
