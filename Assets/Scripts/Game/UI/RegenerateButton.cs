using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class RegenerateButton : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Button _button;

        #endregion

        #region Properties

        public Button Button => _button;

        #endregion
    }
}