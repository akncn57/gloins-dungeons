using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [CreateAssetMenu(fileName = "ScriptableObject_PlayerProperties", menuName = "Scriptable Objects/Player/Player Properties")]
    public class PlayerProperties : ScriptableObject
    {
        public float WalkSpeed;
        public int BasicAttackPower;
        public int HeavyAttackPower;
        public float BasicAttackHitKnockBackPower;
        public float HeavyAttackHitKnockBackPower;
        public float DashForce;
        public float DashTime;
    }
}