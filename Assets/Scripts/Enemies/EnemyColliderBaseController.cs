using System;
using ColliderController;
using HitData;
using UnityEngine;

namespace Enemies
{
    public class EnemyColliderBaseController : ColliderControllerBase
    {
        //TODO: Bagimlilik var.
        [SerializeField] private EnemyBaseStateMachine enemyBaseStateMachine;

        public override void InvokeOnHitStartEvent(int damage, Vector3 knockBackPosition, float knockBackPower)
        {
            base.InvokeOnHitStartEvent(damage, knockBackPosition, knockBackPower);
            enemyBaseStateMachine.HitData = new EnemyHitData(knockBackPosition, damage, knockBackPower);
        }

        public override void InvokeOnHitEndEvent()
        {
            base.InvokeOnHitEndEvent();
        }
    }
}