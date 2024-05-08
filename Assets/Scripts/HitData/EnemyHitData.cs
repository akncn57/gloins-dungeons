using UnityEngine;

namespace HitData
{
    public class EnemyHitData : HitDataBase
    {
        public EnemyHitData(Vector3 hitPosition, int damage, float knockBackStrength) : base(hitPosition, damage, knockBackStrength){}
    }
}