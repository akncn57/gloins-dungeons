﻿using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "ScriptableObject_PlayerProperties", menuName = "Scriptable Objects/Player/Player Properties")]
    public class PlayerProperties : ScriptableObject
    {
        public float WalkSpeed;
        public int BasicAttackPower;
        public float HitKnockBackPower;
    }
}