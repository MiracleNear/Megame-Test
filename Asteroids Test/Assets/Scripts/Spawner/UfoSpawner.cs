using System.Collections;
using DefaultNamespace;
using Enemies;
using Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class UfoSpawner : MonoBehaviour
    {
        [SerializeField] private UfoFactory _ufoFactory;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private int _minAppearanceDelayTime, _maxAppearanceDelayTime;
        [SerializeField] private Collider2D _ufoCollider;
        
        private float _offsetFromTheHorizontalBorderInPercent = 20f;
        private UfoSpawnPoint[] _ufoSpawnPoints;
        private Ufo _activeUfo;
        private Coroutine _spawnUfo;
        private Player _target;

        private void Awake()
        {
            float offset = (ScreenBoundSize.HalfSize.y / 100f) * _offsetFromTheHorizontalBorderInPercent;

            float maxVerticalPositionSpawn = ScreenBoundSize.HalfSize.y - offset;
            float minVerticalPositionSpawn = maxVerticalPositionSpawn;

            float horizontalPosition = ScreenBoundSize.HalfSize.x + _ufoCollider.bounds.size.x;

            _ufoSpawnPoints = new[]
            {
                new UfoSpawnPoint(horizontalPosition, minVerticalPositionSpawn, maxVerticalPositionSpawn, Vector2.left),
                new UfoSpawnPoint(-horizontalPosition, maxVerticalPositionSpawn, maxVerticalPositionSpawn,
                    Vector2.right),
            };
        }
        

        private void OnEnable()
        {
            _playerSpawner.Spawned += OnSpawned;
        }

        private void OnDisable()
        {
            _playerSpawner.Spawned -= OnSpawned;
        }

        private void OnSpawned(Player player)
        {
            player.Died += OnDied;
            _target = player;
            _spawnUfo = StartCoroutine(SpawnWithDelay(GetAppearanceDelayTimeUfo()));
        }

        private void OnDied(Player player)
        {
            if (_activeUfo != null)
            {
                _enemySpawner.UnRegister(_activeUfo);
            }

            if (_spawnUfo != null)
            {
                StopCoroutine(_spawnUfo);

                _spawnUfo = null;
            }
        }

        public void Remove(Ufo ufo)
        {
            _ufoFactory.Reclaim(ufo);
        }

        private IEnumerator SpawnWithDelay(int delay)
        {
            yield return new WaitForSeconds(delay);

            UfoSpawnPoint randomSpawnPoint = GetRandomSpawnPoint();
            
            _activeUfo = _ufoFactory.Create(randomSpawnPoint.GetPosition(), randomSpawnPoint.GetDirection(), _target.transform);
            
            _enemySpawner.Register(_activeUfo);
        }

        private int GetAppearanceDelayTimeUfo()
        {
            return Random.Range(_minAppearanceDelayTime, _maxAppearanceDelayTime + 1);
        }

        private UfoSpawnPoint GetRandomSpawnPoint()
        {
            int randomIndexSpawnPoint = Random.Range(0, _ufoSpawnPoints.Length);

            return _ufoSpawnPoints[randomIndexSpawnPoint];
        }
        

        private class UfoSpawnPoint
        {
            private float _horizontalPosition, _minVerticalPositionSpawn, _maxVerticalPositionSpawn;
            private Vector2 _direction;
            
            public UfoSpawnPoint(float horizontalPosition, float minVerticalPositionSpawn, float maxVerticalPositionSpawn, Vector2 direction)
            {
                _horizontalPosition = horizontalPosition;
                _minVerticalPositionSpawn = minVerticalPositionSpawn;
                _maxVerticalPositionSpawn = maxVerticalPositionSpawn;
                _direction = direction;
            }

            public Vector2 GetPosition()
            {
                float randomVerticalPosition = Random.Range(_minVerticalPositionSpawn, _maxVerticalPositionSpawn);

                return new Vector2(_horizontalPosition, randomVerticalPosition);
            }

            public Vector2 GetDirection()
            {
                return _direction;
            }
        }
    }
}