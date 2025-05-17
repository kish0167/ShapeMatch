using System;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Game.Coins
{
    public class CoinClickDetector : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked; 
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }
}