using UnityEngine;

namespace Zenject.Installers
{
    public class CameraShakeInstaller : MonoInstaller
    {
        [SerializeField] private CameraShake _cameraShake;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_cameraShake).AsSingle().NonLazy();
            Container.QueueForInject(_cameraShake);
        }
    }
}