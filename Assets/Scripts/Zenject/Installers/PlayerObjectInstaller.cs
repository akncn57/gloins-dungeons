using UnityEngine;
using Zenject;

public class PlayerObjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerObject;
    
    public override void InstallBindings()
    {
        Container.Bind<GameObject>().AsSingle().WithArguments(_playerObject);
    }
}
