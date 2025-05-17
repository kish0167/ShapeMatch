using UnityEngine;

namespace Game.Coins
{
    public class Coin : MonoBehaviour
    {
        #region Variables

        [SerializeField] private SpriteRenderer _base;
        [SerializeField] private SpriteRenderer _border;
        [SerializeField] private SpriteRenderer _letter;
        [SerializeField] private CoinClickDetector _clickDetector;

        #endregion

        #region Properties

        public SpriteRenderer Base => _base;
        public SpriteRenderer Border => _border;
        public CoinClickDetector ClickDetector => _clickDetector;
        public SpriteRenderer Letter => _letter;

        #endregion
    }
}