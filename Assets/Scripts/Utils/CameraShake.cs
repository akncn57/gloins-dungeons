using System.Collections;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _perlinNoise;

        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _perlinNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    
        public IEnumerator CameraShakeCor(float intensity, float duration)
        {
            _perlinNoise.m_AmplitudeGain = intensity;
            yield return new WaitForSeconds(duration);
            _perlinNoise.m_AmplitudeGain = 0;
        }
        
        [Button]
        public void TriggerShake(float intensity, float duration)
        {
            StartCoroutine(CameraShakeCor(intensity, duration));
        }
    }
}