using UnityEngine;
using Zenject;

namespace Enemies.Mage
{
    public class MageAttackBasicState : MageBaseState
    {
        private readonly int _attackBasicAnimationHash = Animator.StringToHash("Mage_Attack_Basic");
        
        public MageAttackBasicState(MageStateMachine mageStateMachine, IInstantiator instantiator) : base(mageStateMachine, instantiator){}
        
        public override void OnEnter()
        {
            MageStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapOpen += InstantiateProjectileObject;
            MageStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished += BasicAttackFinish;
            
            MageStateMachine.Rigidbody.linearVelocity = Vector2.zero;
            MageStateMachine.Animator.CrossFadeInFixedTime(_attackBasicAnimationHash, 0.1f);
        }

        public override void OnTick()
        {
            
        }

        public override void OnExit()
        {
            MageStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicOverlapOpen -= InstantiateProjectileObject;
            MageStateMachine.EnemyAnimationEventTrigger.EnemyOnAttackBasicFinished -= BasicAttackFinish;
        }

        private void InstantiateProjectileObject()
        {
            var projectile = Instantiator.InstantiatePrefab(MageStateMachine.ProjectilePrefab, MageStateMachine.ProjectileSpawnPoint.transform);
            projectile.transform.SetParent(GameObject.Find("Projectiles").transform);
        }

        private void BasicAttackFinish()
        {
            MageStateMachine.SwitchState(MageStateMachine.MageIdleState);
        }
    }
}