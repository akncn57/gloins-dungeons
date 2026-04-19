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
        
        private float _shakeTimer;
        private float _shakeTimerTotal;
        private float _startingIntensity;

        private void Start()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _perlinNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        
        [Button]
        public void TriggerShake(float intensity, float duration)
        {
            // Yalnızca yeni shake'in gücü mevcut shake'den büyükse veya shake bitmişse üzerine yaz.
            if (_perlinNoise != null && (intensity >= _perlinNoise.m_AmplitudeGain || _shakeTimer <= 0f))
            {
                _perlinNoise.m_AmplitudeGain = intensity;
                _startingIntensity = intensity;
                _shakeTimerTotal = duration;
                _shakeTimer = duration;
            }
        }

        private void Update()
        {
            if (_shakeTimer > 0)
            {
                _shakeTimer -= Time.deltaTime;
                
                if (_shakeTimer <= 0f)
                {
                    _perlinNoise.m_AmplitudeGain = 0f;
                }
                else
                {
                    // Zaman geçtikçe sarsıntı gücü azalsın (smooth bitiş)
                    _perlinNoise.m_AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
                }
            }
        }
    }
}