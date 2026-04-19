using Zenject.Signals.Combat;

namespace Zenject.Installers
{
    public class SignalBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Zenject.SignalBusInstaller.Install(Container);
            Container.DeclareSignal<TargetChangedSignal>();
            Container.DeclareSignal<HealthChangedSignal>();
        }
    }
}