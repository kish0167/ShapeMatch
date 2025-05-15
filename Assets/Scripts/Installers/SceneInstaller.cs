using Utility;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        #region Public methods

        public override void InstallBindings()
        {
            this.Log("scene context installed");
        }

        #endregion
    }
}