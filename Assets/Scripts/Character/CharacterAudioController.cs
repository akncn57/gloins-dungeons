using Sirenix.OdinInspector;
using UnityEngine;

namespace Character
{
    public class CharacterAudioController : MonoBehaviour
    {
        [SerializeField, Required] private AudioSource audioSource;
        [SerializeField] private CharacterSoundsSo characterSoundsData;
        
        public void PlayLightAttack() => PlaySound(characterSoundsData.lightAttackSound);
        public void PlayHeavyAttack() => PlaySound(characterSoundsData.heavyAttackSound);
        public void PlayDash() => PlaySound(characterSoundsData.dashSound);
        public void PlayHurt() => PlaySound(characterSoundsData.hurtSound);
        public void PlayDeath() => PlaySound(characterSoundsData.deathSound);
        
        public void PlayWalk()
        {
            if (characterSoundsData.walkSounds is not { Length: > 0 }) return;
            
            var randomIndex = Random.Range(0, characterSoundsData.walkSounds.Length);
            PlaySound(characterSoundsData.walkSounds[randomIndex]);
        }
        
        private void PlaySound(AudioClip clip)
        {
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}