using Player;
using UnityEngine;
using Zenject;

public class PlayerStateMachineInstaller : MonoInstaller
{
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerStateMachine>().AsSingle().WithArguments(_playerStateMachine).NonLazy();
    }
}
