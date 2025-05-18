using UnityEngine;

namespace Game.UI
{
    public class Screen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _content;

        #endregion

        #region Public methods

        public void Disable()
        {
            _content.SetActive(false);
        }

        public void Enable()
        {
            _content.SetActive(true);
        }

        #endregion
    }
}