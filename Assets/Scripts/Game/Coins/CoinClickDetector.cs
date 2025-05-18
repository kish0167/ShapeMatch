using System;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Game.Coins
{
    public class CoinClickDetector : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Coin _coin;
        public event Action<Coin> OnClicked;  
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(_coin);
        }
    }
}