using EventInterfaces;

namespace Zenject.Installers
{
    public class SignalBusMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {       
            SignalBusInstaller.Install(Container);
        
            Container.DeclareSignal<IPlayerEvents.OnPlayerHealthChanged>();
        }
    }
}
