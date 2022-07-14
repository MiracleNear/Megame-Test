using System.Collections;
using DefaultNamespace;
using Enemies;
using Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class UfoSpawner : EnemySpawner
    {
        [SerializeField] private UfoFactory _ufoFactory;
        [SerializeField] private int _minAppearanceDelayTime, _maxAppearanceDelayTime;
        [SerializeField] private Collider2D _ufoCollider;
        
        private float _offsetFromTheHorizontalBorderInPercent = 20f;
        private UfoSpawnPoint[] _ufoSpawnPoints;
        private Transform _target;
        private Coroutine _spawnWithDelay;
        private Ufo _activeUfo;

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
        
        public override void Init(Transform target)
		{
            _target = target;

            _spawnWithDelay = StartCoroutine(SpawnWithDelay(GetAppearanceDelayTimeUfo()));
		}

        public override void Dispose()
        {
            StopCoroutine(_spawnWithDelay);

            Remove();
        }


        private void Remove()
        {
            if(_activeUfo != null)
            {
                _ufoFactory.Reclaim(_activeUfo);
            }
        }

        private IEnumerator SpawnWithDelay(int delay)
        {
            yield return new WaitForSeconds(delay);

            UfoSpawnPoint randomSpawnPoint = GetRandomSpawnPoint();
            
            _activeUfo = _ufoFactory.Create(randomSpawnPoint.GetPosition(), randomSpawnPoint.GetDirection(), _target);

            _activeUfo.Died += Remove;
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