using Game.StateMachine.States;
using Utility;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        #region Public methods

        public override void InstallBindings()
        {
            // States
            Container.Bind<BootstrapState>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameOverState>().FromNew().AsSingle().NonLazy();
            Container.Bind<GeneratingFieldState>().FromNew().AsSingle().NonLazy();
            Container.Bind<SolvingState>().FromNew().AsSingle().NonLazy();
            Container.Bind<WinState>().FromNew().AsSingle().NonLazy();
            Container.Bind<ReGeneratingFieldState>().FromNew().AsSingle().NonLazy();
            this.Log("scene context installed");
        }

        #endregion
    }
}