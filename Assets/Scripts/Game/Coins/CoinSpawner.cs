using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
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

        #region Events

        public event Action OnSpawningEnded;

        #endregion

        #region Properties

        public bool IsSpawning => _isSpawning;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(CoinFactory coinFactory)
        {
            _coinFactory = coinFactory;
        }

        #endregion

        #region Public methods

        public void SpawnRegenerated(List<Coin> newCoinsQueue, int count)
        {
            GenerateSpawnQueue(count);

            foreach (Coin coin in newCoinsQueue)
            {
                _spawnQueue.Add(CoinToBlueprint(coin));
            }

            StartCoroutine(SpawningCycle());
        }

        public void StartSpawning()
        {
            if (_isSpawning)
            {
                return;
            }

            GenerateSpawnQueue(_startAmount);
            StartCoroutine(SpawningCycle());
        }

        #endregion

        #region Private methods

        private CoinBlueprint CoinToBlueprint(Coin coin)
        {
            Coin prefab = null;
            foreach (Coin @base in _bases)
            {
                if (@base.Base.sprite != coin.Base.sprite)
                {
                    continue;
                }

                prefab = @base;
                break;
            }

            if (prefab == null)
            {
                this.Error("prefab not found");
                return null;
            }

            return new CoinBlueprint
            {
                baseShapePrefab = prefab,
                color = coin.Base.color,
                letter = coin.Letter.sprite,
            };
        }

        private void GenerateSpawnQueue(int count)
        {
            int tripletsAmount = count / 3;
            _spawnQueue.Clear();

            if (_bases.Count <= 0 || _colors.Count <= 0 ||
                _letters.Count <= 0 || tripletsAmount <= 0)
            {
                return;
            }

            for (int i = 0; i < tripletsAmount; i++)
            {
                CoinBlueprint blueprint = new()
                {
                    baseShapePrefab = _bases[Random.Range(0, _bases.Count)],
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

            while (_spawnQueue.Count > 0)
            {
                float xShift = Random.Range(-transform.localScale.x, transform.localScale.x) / 2;
                float yShift = Random.Range(-transform.localScale.y, transform.localScale.y) / 2;

                CoinBlueprint blueprint = _spawnQueue[Random.Range(0, _spawnQueue.Count)];

                _coinFactory.SpawnCoin(transform.position + new Vector3(xShift, yShift, 0f),
                    blueprint.baseShapePrefab, blueprint.color, blueprint.letter);

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

            public Coin baseShapePrefab;
            public Color color;
            public Sprite letter;

            #endregion
        }

        #endregion
    }
}