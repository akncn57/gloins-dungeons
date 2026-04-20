using UnityEngine;

namespace Enemies.UndeadMage
{
    public class UndeadMageController : EnemyBase
    {
        public UndeadMageCombatController CombatController { get; private set; }
        public float LastLightAttackTime { get; set; } = -100f;
        public float LastHeavyAttackTime { get; set; } = -100f;
        
        private UndeadMageStateMachine _undeadMageStateMachine;

        protected override void Awake()
        {
            CombatController = GetComponent<UndeadMageCombatController>();
            _undeadMageStateMachine = new UndeadMageStateMachine(this);
            StateMachine = _undeadMageStateMachine;
            base.Awake();
        }

        public void OnLightAttackInitAnimationEnd()
        {
            _undeadMageStateMachine.OnLightAttackInitAnimationEnd();
        }

        public void OnLightAttackFinalAnimationEnd()
        {
            _undeadMageStateMachine.OnLightAttackFinalAnimationEnd();
        }
        
        public void OnHeavyAttackAnimationHit()
        {
            _undeadMageStateMachine.OnHeavyAttackAnimationHit();
        }

        public void OnHeavyAttackAnimationEnd()
        {
            _undeadMageStateMachine.OnHeavyAttackAnimationEnd();
        }

        public void OnHurtAnimationEnd()
        {
            _undeadMageStateMachine.OnHurtAnimationEnd();
        }
    }
}