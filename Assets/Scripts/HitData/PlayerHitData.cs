using UnityEngine;

namespace HitData
{
    public class PlayerHitData : HitDataBase
    {
        public PlayerHitData(Vector3 hitPosition, int damage, float knockBackStrength) : base(hitPosition, damage, knockBackStrength){}
    }
}