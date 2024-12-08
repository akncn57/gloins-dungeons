using System.Collections.Generic;
using ColliderController;
using UnityEngine;

namespace Player
{
    public class PlayerAttack
    {
        public List<ColliderControllerBase> HittingEnemies = new();

        public void PlayerOnAttackOpenOverlap(BoxCollider2D attackCollider, int attackPower, float hitKnockBackPower, Vector3 playerPosition)
        {
            var results = Physics2D.OverlapBoxAll(attackCollider.transform.position, attackCollider.size, 0f);
            
            HittingEnemies.Clear();

            if (results == null) return;
            
            foreach (var result in results)
            {
                if (!result) continue;
                var enemy = result.GetComponent<ColliderControllerBase>();
                HittingEnemies.Add(enemy);
                enemy.InvokeOnHitStartEvent(attackPower, (enemy.transform.position - playerPosition).normalized, hitKnockBackPower);
            }
        }
        
        public void PlayerOnAttackCloseOverlap()
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