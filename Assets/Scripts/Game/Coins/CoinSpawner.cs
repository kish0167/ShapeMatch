using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Coins
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private List<CoinBase> _bases;
        [SerializeField] private List<Color> _colors;
        [SerializeField] private List<Sprite> _letters;
        [SerializeField] private int _startAmount;

        private CoinFactory _coinFactory;


        [Inject]
        private void Construct(CoinFactory coinFactory)
        {
            _coinFactory = coinFactory;
        }
        
        public void StartSpawning()
        {
            StartCoroutine(SpawningCycle());
        }

        IEnumerator SpawningCycle()
        {
            for (int i = 0; i < _startAmount; i++)
            {
                _coinFactory.SpawnCoin(transform.position, _bases[0], _colors[0], _letters[0]);
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}