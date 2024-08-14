using System.Collections.Generic;
using ColliderController;
using UnityEngine;

namespace Player
{
    public class PlayerAttackBasic
    {
        public List<ColliderControllerBase> HittingEnemies = new();

        public void PlayerOnAttackBasicOpenOverlap(CapsuleCollider2D attackCollider, int attackPower, float hitKnockBackPower, Vector3 playerPosition)
        {
            var results = Physics2D.OverlapCapsuleAll(attackCollider.transform.position, attackCollider.size, attackCollider.direction, 0f);
            
            HittingEnemies.Clear();
            
            foreach (var result in results)
            {
                if (!result) continue;
                var enemy = result.GetComponent<ColliderControllerBase>();
                HittingEnemies.Add(enemy);
                enemy.InvokeOnHitStartEvent(attackPower, (enemy.transform.position - playerPosition).normalized, hitKnockBackPower);
            }
        }
        
        public void PlayerOnAttackBasicCloseOverlap()
        {
            foreach (var enemy in HittingEnemies)
            {
                if (!enemy) continue;
                enemy.InvokeOnHitEndEvent();
            }
            
            HittingEnemies.Clear();
        }
    }
}