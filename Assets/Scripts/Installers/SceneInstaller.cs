using Game.Coins;
using Game.StateMachine;
using Game.StateMachine.States;
using UnityEngine;
using Utility;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private CoinSpawner _coinSpawner;
        
        #region Public methods

        public override void InstallBindings()
        {
            // Coins
            Container.Bind<CoinSpawner>().FromInstance(_coinSpawner).AsSingle().NonLazy();
            Container.Bind<CoinFactory>().FromNew().AsSingle().NonLazy();
            
            // States
            Container.Bind<LocalStateMachine>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
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