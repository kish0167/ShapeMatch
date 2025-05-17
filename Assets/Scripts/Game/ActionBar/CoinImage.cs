using UnityEngine;
using UnityEngine.UI;

namespace Game.ActionBar
{
    public class CoinImage : MonoBehaviour
    {
        [SerializeField] private Image _baseImage;
        [SerializeField] private Image _borderImage;
        [SerializeField] private Image _letterImage;

        public void SetSpritesAndColor(Sprite @base, Sprite border, Sprite letter, Color color)
        {
            _baseImage.sprite = @base;
            _baseImage.color = color;
            _borderImage.sprite = border;
            _letterImage.sprite = letter;
        }
    }
}