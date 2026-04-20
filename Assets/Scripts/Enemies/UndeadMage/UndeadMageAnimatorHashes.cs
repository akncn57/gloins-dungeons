using UnityEngine;

namespace Enemies.UndeadMage
{
    public static class UndeadMageAnimatorHashes
    {
        public static readonly int LightAttackInit = Animator.StringToHash("lightAttackInit");
        public static readonly int LightAttackFinal = Animator.StringToHash("lightAttackFinal");
        public static readonly int HeavyAttack = Animator.StringToHash("heavyAttack");
        public static readonly int Hurt = Animator.StringToHash("hurt");
        public static readonly int Death = Animator.StringToHash("death");
        public static readonly int IsWalking = Animator.StringToHash("isWalking");
    }
}