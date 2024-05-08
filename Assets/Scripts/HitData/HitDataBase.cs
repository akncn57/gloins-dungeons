using UnityEngine;

namespace HitData
{
    public abstract class HitDataBase : MonoBehaviour
    {
        public Vector3 HitPosition { get; set; }
        public int Damage { get; set; }
        public float KnockBackStrength{ get; set; }
        
        public HitDataBase(Vector3 hitPosition, int damage, float knockBackStrength)
        {
            HitPosition = hitPosition;
            Damage = damage;
            KnockBackStrength = knockBackStrength;
        }
    }
}
