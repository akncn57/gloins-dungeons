using UtilScripts;

namespace Zenject.Installers
{
    public class CoroutineRunnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CoroutineRunner>().FromMethod(ctx => ctx.Container.InstantiateComponent<CoroutineRunner>(gameObject)).AsSingle().NonLazy();
        }
    }
}