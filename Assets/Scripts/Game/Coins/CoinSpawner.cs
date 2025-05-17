using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree.Util;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Coins
{
    public class CoinSpawner : MonoBehaviour
    {
        #region Variables

        [SerializeField] private List<Coin> _bases;
        [SerializeField] private List<Color> _colors;
        [SerializeField] private List<Sprite> _letters;
        [SerializeField] private int _startAmount;
        [SerializeField] private float _coinsPerSecond;

        private readonly List<CoinBlueprint> _spawnQueue = new();

        private CoinFactory _coinFactory;
        private bool _isSpawning;

        #endregion

        #region Properties

        public bool IsSpawning => _isSpawning;

        #endregion
        public event Action OnSpawningEnded; 

        #region Setup/Teardown

        [Inject]
        private void Construct(CoinFactory coinFactory)
        {
            _coinFactory = coinFactory;
        }

        #endregion

        #region Public methods

        public void StartSpawning()
        {
            if (_isSpawning)
            {
                return;
            }

            GenerateSpawnQueue();
            StartCoroutine(SpawningCycle());
        }

        #endregion

        #region Private methods

        private void GenerateSpawnQueue()
        {
            int tripletsAmount = _startAmount / 3;
            _spawnQueue.Clear();

            if (_bases.Count <= 0 || _colors.Count <= 0 || _letters.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < tripletsAmount; i++)
            {
                CoinBlueprint blueprint = new()
                {
                    baseShape = _bases[Random.Range(0, _bases.Count)],
                    color = _colors[Random.Range(0, _colors.Count)],
                    letter = _letters[Random.Range(0, _letters.Count)],
                };

                _spawnQueue.Add(blueprint);
                _spawnQueue.Add(blueprint);
                _spawnQueue.Add(blueprint);
            }
        }

        private IEnumerator SpawningCycle()
        {
            _isSpawning = true;

            /*foreach (CoinBlueprint blueprint in _spawnQueue)
            {
                float xShift = Random.Range(-transform.localScale.x, transform.localScale.x) / 2;
                float yShift = Random.Range(-transform.localScale.y, transform.localScale.y) / 2;

                _coinFactory.SpawnCoin(transform.position + new Vector3(xShift, yShift, 0f),
                    blueprint.baseShape, blueprint.color, blueprint.letter);

                yield return new WaitForSeconds(1 / _coinsPerSecond);
            }*/

            while (_spawnQueue.Count > 0)
            {
                float xShift = Random.Range(-transform.localScale.x, transform.localScale.x) / 2;
                float yShift = Random.Range(-transform.localScale.y, transform.localScale.y) / 2;

                CoinBlueprint blueprint = _spawnQueue[Random.Range(0, _spawnQueue.Count)];

                _coinFactory.SpawnCoin(transform.position + new Vector3(xShift, yShift, 0f),
                    blueprint.baseShape, blueprint.color, blueprint.letter);

                _spawnQueue.Remove(blueprint);

                yield return new WaitForSeconds(1 / _coinsPerSecond);
            }

            _isSpawning = false;
            
            OnSpawningEnded?.Invoke();
        }

        #endregion

        #region Local data

        private class CoinBlueprint
        {
            #region Variables

            public Coin baseShape;
            public Color color;
            public Sprite letter;

            #endregion
        }

        #endregion
    }
}