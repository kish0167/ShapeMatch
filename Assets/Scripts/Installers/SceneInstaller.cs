using Game.ActionBar;
using Game.Coins;
using Game.StateMachine;
using Game.StateMachine.States;
using Game.UI;
using UnityEngine;
using Utility;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private CoinSpawner _coinSpawner;
        [SerializeField] private ActionBar _actionBar;
        [SerializeField] private WinScreen _winScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private RegenerateButton _regenerateButton;
        
        
        
        #region Public methods

        public override void InstallBindings()
        {
            // Buttons
            Container.Bind<RegenerateButton>().FromInstance(_regenerateButton).AsSingle().NonLazy();
            
            // Screens
            Container.Bind<GameOverScreen>().FromInstance(_gameOverScreen).AsSingle().NonLazy();
            Container.Bind<WinScreen>().FromInstance(_winScreen).AsSingle().NonLazy();
            
            // Action bar
            Container.Bind<ActionBar>().FromInstance(_actionBar).AsSingle().NonLazy();
            Container.Bind<ActionBarService>().FromNew().AsSingle().NonLazy();
            
            // Coins
            Container.Bind<CoinSpawner>().FromInstance(_coinSpawner).AsSingle().NonLazy();
            Container.Bind<CoinFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<CoinsService>().FromNew().AsSingle().NonLazy();
            Container.Bind<RegenerationService>().FromNew().AsSingle().NonLazy();
            
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