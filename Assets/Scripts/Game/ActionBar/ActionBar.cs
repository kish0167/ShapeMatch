using System.Collections.Generic;
using Game.Coins;
using UnityEngine;

namespace Game.ActionBar
{
    public class ActionBar : MonoBehaviour
    {
        #region Variables

        [SerializeField] private List<CoinImage> _coins;

        #endregion

        #region Properties

        public int Capacity => _coins.Count;

        #endregion

        #region Public methods

        public void Set(List<Coin> coins)
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (i < coins.Count)
                {
                    _coins[i].gameObject.SetActive(true);
                    TransferSprites(coins[i], _coins[i]);
                }
                else
                {
                    _coins[i].gameObject.SetActive(false);
                }
            }
        }

        #endregion

        #region Private methods

        private void TransferSprites(Coin coin, CoinImage image)
        {
            image.SetSpritesAndColor(coin.Base.sprite, coin.Border.sprite,
                coin.Letter.sprite, coin.Base.color);
        }

        #endregion
    }
}