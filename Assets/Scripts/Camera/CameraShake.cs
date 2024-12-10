using System.Collections;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin _perlinNoise;

    private void Start()
    {
        _perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    public IEnumerator CameraShakeCor(float intensity, float duration)
    {
        _perlinNoise.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        _perlinNoise.m_AmplitudeGain = 0;
    }

    #region Test Methods

    [Button]
    public void TriggerShake(float intensity, float duration)
    {
        StartCoroutine(CameraShakeCor(intensity, duration));
    }

    #endregion
}